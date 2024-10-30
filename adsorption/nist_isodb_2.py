# -*- coding: utf-8 -*-
"""NIST-ISODB-2.ipynb

Automatically generated by Colab.

Original file is located at
    https://colab.research.google.com/drive/1fJkiwlj1IncMoDQRjotIIa5GkgMK8CpD
"""

!pip install gitpython

import git
import os
import json
import pandas as pd

#@title #Clone Repository
# Define the repository URL and the directory to clone into
repo_url = "https://github.com/NIST-ISODB/isodb-library"
clone_dir = "./isodb-library"

# Check if the directory already exists
if not os.path.exists(clone_dir):
    try:
        # Clone the repository into the specified directory
        git.Repo.clone_from(repo_url, clone_dir)
        print(f"Repository cloned into {clone_dir}")
    except Exception as e:
        print(f"Error cloning repository: {e}")
else:
    print(f"Repository already exists at {clone_dir}")

# Optionally, open the cloned repository for further operations
try:
    repo = git.Repo(clone_dir)
    # Print the latest commit
    print(f"Latest commit: {repo.head.commit}")
except Exception as e:
    print(f"Error accessing the repository: {e}")

#@title #Get Data
# Define the path to the cloned repository
repo_path = './isodb-library'

# Function to list all JSON files in the given directory and subdirectories
def list_json_files(directory):
    json_files = []
    for root, dirs, files in os.walk(directory):
        for file in files:
            if file.endswith(".json"):
                json_files.append(os.path.join(root, file))
    return json_files

# Function to read and print the content of a JSON file
def read_json_file(file_path):
    with open(file_path, 'r', encoding='utf-8') as f:
        data = json.load(f)
        return data

# List all JSON files in the repository
json_files = list_json_files(repo_path)

# Print the first few JSON files
print(f"Found {len(json_files)} JSON files. Example files:")
for i, file_path in enumerate(json_files[:5]):
    print(f"{i+1}. {file_path}")

# Read the content of the first JSON file as an example
if json_files:
    example_file = json_files[0]
    data = read_json_file(example_file)
    print("\nContent of the first JSON file:")
    print(json.dumps(data, indent=2))

#@title #Generate Excel File
# Define the paths to the cloned repository
repo_path = './isodb-library'
adsorbates_path = './isodb-library/Library/Adsorbates'

# Function to load a JSON file and return its content
def load_json(file_path):
    with open(file_path, 'r', encoding='utf-8') as f:
        return json.load(f)

# Function to extract relevant isotherm data from the JSON file
def extract_isotherm_data(file_path, adsorbate_map):
    with open(file_path, 'r', encoding='utf-8') as f:
        data = json.load(f)

        # Check if the file has 'isotherm_data' key, skip if not present
        if 'isotherm_data' not in data:
            return []

        # Flatten the data into rows for each pressure point
        rows = []
        for entry in data['isotherm_data']:
            for species in entry['species_data']:
                # Get adsorbate name from InChIKey
                adsorbate_name = adsorbate_map.get(species['InChIKey'], 'Unknown')

                # Prepare the row of data
                row = {
                    'DOI': data['DOI'],
                    'File_Name': os.path.basename(file_path),  # Add file name to each row
                    'Adsorbate_InChIKey': species['InChIKey'],
                    'Adsorbate_Name': adsorbate_name,  # Add adsorbate name
                    'Adsorbate_Adsorption': species['adsorption'],
                    'Pressure': entry['pressure'],
                    'Total_Adsorption': entry['total_adsorption'],
                    'Temperature': data.get('temperature', None),
                    'Adsorbent_HashKey': data['adsorbent']['hashkey'],
                    'Adsorbent_Name': data['adsorbent']['name'],
                    'Pressure_Units': data.get('pressureUnits', None),
                    'Adsorption_Units': data.get('adsorptionUnits', None),
                }

                # Additional keys that may be present in mixtures (e.g., 'composition')
                if 'composition' in species:
                    row['Adsorbate_Composition'] = species['composition']
                else:
                    row['Adsorbate_Composition'] = None

                rows.append(row)
        return rows

# Function to load adsorbates data into a dictionary
def load_adsorbates(directory):
    adsorbate_map = {}
    for root, dirs, files in os.walk(directory):
        for file in files:
            if file.endswith(".json"):
                file_path = os.path.join(root, file)
                try:
                    with open(file_path, 'r', encoding='utf-8') as f:
                        data = json.load(f)
                        adsorbate_map[data['InChIKey']] = data['name']
                except Exception as e:
                    print(f"Error processing adsorbate file {file_path}: {e}")
    return adsorbate_map

# Step 1: Load the adsorbate data into a dictionary
adsorbate_map = load_adsorbates(adsorbates_path)

# Step 2: Collect isotherm data from all JSON files
isotherm_files = []
for root, dirs, files in os.walk(repo_path):
    for file in files:
        if file.endswith(".json"):
            isotherm_files.append(os.path.join(root, file))

all_data = []

# Step 3: Extract data from each isotherm JSON file
for json_file in isotherm_files:
    try:
        rows = extract_isotherm_data(json_file, adsorbate_map)
        if rows:  # Only append if the file had isotherm_data
            all_data.extend(rows)
    except Exception as e:
        print(f"Error processing file {json_file}: {e}")

# Step 4: Convert the collected data into a DataFrame
df = pd.DataFrame(all_data)

# Display the first few rows of the DataFrame
print(df.head())

# Optionally, save the DataFrame to CSV or Excel for further analysis
#df.to_csv('isotherm_data_with_mixtures.csv', index=False)
df.to_excel('isotherm_data_with_mixtures.xlsx', index=False)

#@title #Esitimate Langmuir isotherm and Maximum Adsorption

# Example: Specify the file path of the JSON file
#file_path = './isodb-library/Library/10.1021la034766z/10.1021la034766z.Isotherm6.json'
#file_path = './isodb-library/Library/10.1021la034766z/10.1021la034766z.Isotherm11.json'
file_path ='./isodb-library/Library/10.1007s1045001293862/10.1007s10450-012-9386-2.isotherm1.json'
#file_path = './isodb-library/Library/10.1002ceat.270170507/10.1002ceat.270170507.Isotherm21.json'

import json
import numpy as np
import matplotlib.pyplot as plt
from scipy.optimize import curve_fit

# Define the Langmuir isotherm equation
def langmuir_isotherm(P, q_max, K):
    return (q_max * K * P) / (1 + K * P)

# Load the JSON data
def load_json_data(file_path):
    with open(file_path, 'r', encoding='utf-8') as f:
        data = json.load(f)
    return data

# Extract the pressure and adsorption data for each species
def extract_species_data(data, species_inchikey):
    pressures = []
    adsorptions = []
    for entry in data['isotherm_data']:
        pressure = entry['pressure']
        for species in entry['species_data']:
            if species['InChIKey'] == species_inchikey:
                pressures.append(pressure)
                adsorptions.append(species['adsorption'])
    return np.array(pressures), np.array(adsorptions)

# Perform Langmuir isotherm fitting for a given species
def fit_langmuir(pressures, adsorptions):
    # Initial guesses for q_max and K
    initial_guess = [np.max(adsorptions), 0.001]
    popt, pcov = curve_fit(langmuir_isotherm, pressures, adsorptions, p0=initial_guess)
    q_max, K = popt
    return q_max, K

# Plot the fit and include adsorbent name and DOI
def plot_langmuir_fit(pressures, adsorptions, q_max, K, species_name, adsorbent_name, doi, temperature, adsorption_units):
    P_fit = np.linspace(min(pressures), max(pressures), 100)
    q_fit = langmuir_isotherm(P_fit, q_max, K)

    plt.scatter(pressures, adsorptions, label=f"Experimental {species_name}")
    plt.plot(P_fit, q_fit, label=f"Langmuir Fit {species_name}\nq_max={q_max:.4f} {adsorption_units}, K={K:.6f}", color="red")
    plt.xlabel("Pressure (bar)")
    plt.ylabel(f"Adsorption ({adsorption_units})")
    plt.title(f"Langmuir Isotherm Fit for {species_name}")
    plt.suptitle(f"Adsorbent: {adsorbent_name}   Temperature: {temperature} K   DOI: {doi}", fontsize=10)
    plt.legend()
    plt.show()

# Function to dynamically create the species_data dictionary for mixtures
def create_species_data(data):
    species_data = {}
    for adsorbate in data['adsorbates']:
        species_name = adsorbate['name']
        inchikey = adsorbate['InChIKey']
        species_data[species_name] = inchikey
    return species_data

# Main function to handle both pure and mixture data
def main(file_path):
    # Load the JSON data
    data = load_json_data(file_path)

    # Extract the adsorbent name, DOI, and temperature
    adsorbent_name = data['adsorbent']['name']
    doi = data['DOI']
    temperature = data['temperature']

    # Extract the adsorption units (default to mmol/g if not found)
    adsorption_units = data['adsorptionUnits']

    # Dynamically create species_data from JSON
    species_data = create_species_data(data)

    for species_name, inchikey in species_data.items():
        pressures, adsorptions = extract_species_data(data, inchikey)

        # Filter out zero adsorption data points for fitting
        valid_indices = adsorptions > 0
        pressures = pressures[valid_indices]
        adsorptions = adsorptions[valid_indices]

        # Fit the Langmuir isotherm
        q_max, K = fit_langmuir(pressures, adsorptions)

        # Plot the fit and include adsorbent name, DOI, and adsorption units
        plot_langmuir_fit(pressures, adsorptions, q_max, K, species_name, adsorbent_name, doi, temperature, adsorption_units)

        # Output the results
        print(f"{species_name}: q_max = {q_max:.4f} {adsorption_units}, K = {K:.6f} 1/bar")
        print(f"Adsorbent: {adsorbent_name}")
        print(f"DOI: {doi}")


main(file_path)

#@title #Seach for a Component

# Define the adsorbate name you want to search for
adsorbate_name = "Methane"  # You can change this to search for other adsorbates

# Define the path to the cloned repository
repo_path = './isodb-library'

# Function to list all JSON files in the given directory and subdirectories, excluding "Bibliography"
def list_json_files(directory):
    json_files = []
    for root, dirs, files in os.walk(directory):
        for file in files:
            if file.endswith(".json") and "Bibliography" not in root:
                json_files.append(os.path.join(root, file))
    return json_files

# Function to read and check if a specific adsorbate is in the JSON file
def contains_adsorbate(file_path, adsorbate_name):
    with open(file_path, 'r', encoding='utf-8') as f:
        data = json.load(f)
        # Check if the given adsorbate name is in the adsorbates list
        for adsorbate in data.get("adsorbates", []):
            if adsorbate.get("name") == adsorbate_name:
                return True
    return False

# Function to get the list of all JSON files where a specific adsorbate is present
def search_for_adsorbate_in_files(directory, adsorbate_name):
    json_files = list_json_files(directory)
    matching_files = []

    for json_file in json_files:
        if contains_adsorbate(json_file, adsorbate_name):
            matching_files.append(json_file)

    return matching_files

# Main search logic
def main():

    # Search for JSON files where the adsorbate is present
    matching_files = search_for_adsorbate_in_files(repo_path, adsorbate_name)

    # Print the result
    print(f"Found {len(matching_files)} JSON files where {adsorbate_name} is an adsorbate:")
    for file in matching_files:
        print(file)

# Run the main function
main()

#@title #Seach for a Component with Multiple Temperature Data

# Define the adsorbate name you want to search for
adsorbate_name = "Methane"  # You can change this to search for other adsorbates

import os
import json

# Define the path to the cloned repository
repo_path = './isodb-library'

# Function to list all JSON files in the given directory and subdirectories, excluding "Bibliography"
def list_json_files(directory):
    json_files = []
    for root, dirs, files in os.walk(directory):
        for file in files:
            if file.endswith(".json") and "Bibliography" not in root:
                json_files.append(os.path.join(root, file))
    return json_files

# Function to read and check if the adsorbate, adsorbent, and DOI match the target criteria, and ensure it's pure
def extract_relevant_data(file_path, adsorbate_name):
    with open(file_path, 'r', encoding='utf-8') as f:
        data = json.load(f)

    # Extract required data
    adsorbates = data.get("adsorbates", [])
    adsorbent_name = data.get("adsorbent", {}).get("name", "")
    doi = data.get("DOI", "")
    temperature = data.get("temperature", None)

    # Check if it's pure (only one adsorbate)
    if len(adsorbates) == 1 and adsorbates[0].get("name") == adsorbate_name:
        return {"file_path": file_path, "adsorbent": adsorbent_name, "doi": doi, "temperature": temperature}

    return None

# Function to find files with the same adsorbate, adsorbent, and DOI but different temperatures, only for pure Methane
def search_for_same_system_different_temps(directory, adsorbate_name):
    json_files = list_json_files(directory)
    relevant_data = []

    for json_file in json_files:
        data = extract_relevant_data(json_file, adsorbate_name)
        if data:
            relevant_data.append(data)

    # Now filter for the same adsorbent and DOI but different temperatures
    system_map = {}

    for entry in relevant_data:
        key = (entry['adsorbent'], entry['doi'])
        if key not in system_map:
            system_map[key] = []
        system_map[key].append(entry)

    # Filter entries where we have multiple temperatures for the same adsorbent and DOI
    results = []
    for key, entries in system_map.items():
        if len(set(entry['temperature'] for entry in entries)) > 1:  # Different temperatures
            results.append(entries)

    return results

# Main search logic
def main():

    # Search for matching files with same DOI, adsorbent, but different temperatures
    results = search_for_same_system_different_temps(repo_path, adsorbate_name)

    # Count the total number of JSON files (isotherms)
    total_isotherms = sum(len(system) for system in results)

    # Print the result
    print(f"Found {len(results)} sets of data with different temperatures for {adsorbate_name}:")
    print(f"Total number of JSON files (isotherms) in these sets: {total_isotherms}\n")

    for system_data in results:
        print(f"Adsorbent: {system_data[0]['adsorbent']}, DOI: {system_data[0]['doi']}")
        for entry in system_data:
            print(f"  - File: {entry['file_path']}, Temperature: {entry['temperature']} K")

# Run the main function
main()

#@title #Esitimate Langmuir isotherm, Maximum Adsorption and Energy of Adsorption

# Add more file paths for other temperatures here
#file_paths = [
        #'./isodb-library/Library/10.1039c2ta00155a/10.1039C2ta00155a.isotherm4.json',  # 273 K
        #'./isodb-library/Library/10.1039c2ta00155a/10.1039C2ta00155a.isotherm7.json'  ] # 296 K

file_paths = [
    "./isodb-library/Library/10.1016j.carbon.2013.07.061/10.1016j.carbon.2013.07.061.Isotherm28.json",
    "./isodb-library/Library/10.1016j.carbon.2013.07.061/10.1016j.carbon.2013.07.061.Isotherm26.json",
    "./isodb-library/Library/10.1016j.carbon.2013.07.061/10.1016j.carbon.2013.07.061.Isotherm25.json"]



import json
import numpy as np
import matplotlib.pyplot as plt
from scipy.optimize import curve_fit

# Define the Langmuir isotherm equation
def langmuir_isotherm(P, q_max, K):
    return (q_max * K * P) / (1 + K * P)

# Load the JSON data
def load_json_data(file_path):
    with open(file_path, 'r', encoding='utf-8') as f:
        data = json.load(f)
    return data

# Extract the pressure and adsorption data from a JSON file
def extract_isotherm_data(data):
    pressures = []
    adsorptions = []
    for entry in data['isotherm_data']:
        pressure = entry['pressure']
        adsorption = entry['species_data'][0]['adsorption']  # Assuming only one adsorbate
        pressures.append(pressure)
        adsorptions.append(adsorption)
    return np.array(pressures), np.array(adsorptions)

# Perform Langmuir isotherm fitting for a given dataset
def fit_langmuir(pressures, adsorptions):
    # Initial guesses for q_max and K
    initial_guess = [np.max(adsorptions), 0.001]
    popt, pcov = curve_fit(langmuir_isotherm, pressures, adsorptions, p0=initial_guess)
    q_max, K = popt
    return q_max, K

# Plot the Langmuir isotherm fit
def plot_langmuir_fit(pressures, adsorptions, q_max, K, temperature):
    P_fit = np.linspace(min(pressures), max(pressures), 100)
    q_fit = langmuir_isotherm(P_fit, q_max, K)

    plt.scatter(pressures, adsorptions, label=f"Experimental at {temperature} K")
    plt.plot(P_fit, q_fit, label=f"Langmuir Fit\nq_max={q_max:.4f}, K={K:.6f}", color="red")
    plt.xlabel("Pressure (bar)")
    plt.ylabel("Adsorption (mmol/g)")
    plt.title(f"Langmuir Isotherm Fit at {temperature} K")
    plt.legend()
    plt.show()

# Van't Hoff plot to estimate delta H
def estimate_delta_H(K_values, temperatures):
    # Convert temperatures to 1/T
    inverse_temperatures = 1 / np.array(temperatures)
    ln_K_values = np.log(K_values)

    # Perform linear regression (Van't Hoff plot)
    slope, intercept = np.polyfit(inverse_temperatures, ln_K_values, 1)
    R = 8.314  # Gas constant J/(mol·K)
    delta_H = -slope * R  # Calculate delta H

    # Plot the Van't Hoff plot
    plt.scatter(inverse_temperatures, ln_K_values, label="Data")
    plt.plot(inverse_temperatures, slope * inverse_temperatures + intercept, label=f"Fit: Slope = {slope:.4f}", color="red")
    plt.xlabel("1/T (1/K)")
    plt.ylabel("ln(K)")
    plt.title("Van't Hoff Plot")
    plt.legend()
    plt.show()

    return delta_H

# Main function to perform the Langmuir fitting and estimate delta H
def main():
    # Example file paths for the isotherms at different temperatures (can scale with more files)

    temperatures = []
    K_values = []
    q_max_values = []

    # Loop through all file paths to load, fit, and store results
    for file_path in file_paths:
        # Load the isotherm data
        data = load_json_data(file_path)

        # Extract the temperature from the JSON file
        temperature = data['temperature']
        temperatures.append(temperature)

        # Extract pressure and adsorption data
        pressures, adsorptions = extract_isotherm_data(data)
        adsorption_units = data['adsorptionUnits']

        # Perform Langmuir isotherm fitting
        q_max, K = fit_langmuir(pressures, adsorptions)

        # Store the results
        K_values.append(K)
        q_max_values.append(q_max)

        # Plot the Langmuir fit
        plot_langmuir_fit(pressures, adsorptions, q_max, K, temperature)

    # Estimate delta H using the Van't Hoff equation if more than one temperature is available
    if len(temperatures) > 1:
        delta_H = estimate_delta_H(K_values, temperatures)

        # Output the results
        for temp, q_max, K in zip(temperatures, q_max_values, K_values):
            print(f"Temperature {temp} K: q_max = {q_max:.4f} {adsorption_units}, K = {K:.6f} 1/bar")
        print(f"Estimated Delta H (ΔH) = {delta_H:.4f} J/mol")
    else:
        print("Not enough temperatures to calculate delta H (ΔH).")

# Run the main function
main()

#@title #Others
import json
import os

# Define the path to the specific JSON file
#file_path = './isodb-library/Library/10.1021la034766z/10.1021la034766z.Isotherm11.json'
file_path = './isodb-library/Library/10.1039c2jm15734a/10.1039C2jm15734a.Isotherm2.json'


# Function to load and print the entire JSON content
def load_and_print_json(file_path):
    with open(file_path, 'r', encoding='utf-8') as f:
        data = json.load(f)
        print(json.dumps(data, indent=2))  # Pretty print with indentation

# Load and print the content of the JSON file
load_and_print_json(file_path)

#@title #Others
import json
import os

# Define the path to the specific JSON file
#file_path = './isodb-library/Library/10.1021la034766z/10.1021la034766z.Isotherm11.json'
file_path = './isodb-library/Library/10.1039c2jm15734a/10.1039C2jm15734a.Isotherm1.json'


# Function to load and print the entire JSON content
def load_and_print_json(file_path):
    with open(file_path, 'r', encoding='utf-8') as f:
        data = json.load(f)
        print(json.dumps(data, indent=2))  # Pretty print with indentation

# Load and print the content of the JSON file
load_and_print_json(file_path)