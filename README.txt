
# JSON Conversion Utility  

Welcome to the JSON Conversion Utility!  

This tool simplifies the process of converting JSON data into **CSV** files or directly inserting it into a **database**, helping to boost productivity for QA resources and other teams.  

---

## Features  
- **JSON to CSV Conversion**: Easily convert JSON data into structured CSV files.  
- **JSON to Database Insertion**: Insert JSON data directly into your database tables for streamlined data management.  

---

## Configuration  

### AppConfig Settings  
To configure the utility, update the following keys in the `AppConfig` file:  

1. **Convert JSON to CSV**  
   - Set the key `ImportDataIntoCSV` to `true` to enable JSON to CSV conversion.  

2. **Insert JSON into Database**  
   - Set the key `ImportDataIntoSQL` to `true` to enable JSON data insertion into your database.  

3. **Database Table Names**  
   - Define the following keys with your table names to specify where the data should be inserted:  
     - `tableDocument`  
     - `tableDealpoint`  
     - `tableGoverningLaw`  
     - `tableJurisdiction`  
     - `tablePartyInvolved`  

4. **Build ID (Optional)**  
   - Set the key `buildId-Integer` if you want to include a version number with your entries.  

---

## Usage  

1. Update the `AppConfig` file with the required settings (as described above).  
2. Run the utility to automatically process JSON files according to your configuration.  

---

## Benefits  
- Streamlines JSON data handling for QA and development teams.  
- Provides flexibility to export data in CSV format or store it in a database.  
- Saves time and reduces manual effort in data transformation.  

---

## Support  

If you have any questions or feedback, feel free to reach out. Thank you for using the JSON Conversion Utility!  
