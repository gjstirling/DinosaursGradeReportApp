# About 
This coding excercise was my first task building an API using C# and .NET. Here are a few learning points I discovered: 

- Data should always be wrapped inside a Data Transfer object. This provdes the necessary structure to move the data around the application consistenly. This DTO extracts the necessary data from the Enitity in order to return a response to the user. 
- Enumerations make for clearer and more readable code, particularly when meaningful names are used. In my case to identify months by their name and date.  

# Coding Exercise
Description:
A school of dinosaurs have been performing all their end of year grade calculations from a spreadsheet.
They have enough money left in the budget to develop a simple app that should handle these calculations for them.
A CSV file of dinosaurs and their results has been provided in the API's wwwroot folder.
We have provided everything you need to get the app running and the database initialized (it's just a local SQLite database).
If you're struggling with getting the data in, just move on to the next question.

The challenge:

1. Let's get that data into a database!
- Create the data structure for the provided data in code, and scaffold the database migration. Feel free to add more tables if you like. We've added a 'Dinosaur' entity for you to get started.
- Now import the data from the CSV and insert the data into the database. We've added in a placeholder initializer to get you started.

2. Let's get that API ready to serve the data!
- We've added a skeleton API project but you'll need to build some controllers or endpoints to provide the data.

3. Let's query that data now!
- Implement an endpoint that provides a list of each class, along with the highest and lowest grade.
- Implement an endpoint that provides a list of each class, along with the average score for each dinosaur.
- Some dinosaurs were unable to provide scores for every test, provide the option to exclude these dinosaurs from the above lists.
- Teachers have requested the ability to find a dinosaur by name (including their scores) - implement an endpoint that does this.

4. Let's build some UI to show it! (very much optional, it might be fun!?)
- Display all the dinosaurs in a table, with their average score, and teacher name.
- Clicking on a record in the table should return a page that contains all their scores by month.
- Implement a search bar that takes a name and then returns all matching search results, along with their scores.
