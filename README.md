**To run this project locally, you need:**

1. Clone using the web URL (https://github.com/yaroslav-lysenko-axon/InventorSoftTest.git) or use a password-protected SSH key (git@github.com:yaroslav-lysenko-axon/InventorSoftTest.git) and open in your IDE via "Get from Version Control" menu;
2. Find in InventorSoftTestApp.Host -> Properties -> launchSettings.json. Click Run Profile against the 4th row;
3. Change connection string in appsettings.json to your credentials to connect to the sql server;
4. Press button "Run" in IDE. The migrations will be completed automatically. After that you can find VersionInfo, task and user table directly into dbo in your database;
5. Go by this link "http://localhost:5001/swagger/index.html" to Browser (to use Swagger);

**There are four http methods to:**
1. Create task (POST);
2. Get tasks (GET);
3. Create user (POST);
4. Get user (GET);

**Also I have added here the background job via hangfire. There is logic for the follow requirements:**
 - Every 2 minutes all tasks should be reassigned to another random user (it can’t be the user which is already assigned to the task);
 - When no users are available the Task will stay without assigned user;
 - All task have to be transferred for exactly 3 times, after that, they should be considered completed and stay unassigned.

**And I made a decision about the next cases by myself:**
- A task can be assigned to the same user, but cannot be assigned 2 times in a row;
- A task will be considered 'Completed' and stay unassigned the next time the background job is started after it has been assigned to users 3 times.

**Screenshoots:**
1) Create task:

2) Get tasks:

3) Create user:

4) Get users:


