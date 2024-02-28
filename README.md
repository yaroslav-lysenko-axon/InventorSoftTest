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
![image](https://github.com/yaroslav-lysenko-axon/InventorSoftTest/assets/88324041/76dd3633-b560-4a51-953b-243f8e55066b)
2) Get tasks:
![image](https://github.com/yaroslav-lysenko-axon/InventorSoftTest/assets/88324041/4a83ce7c-e6f8-4307-981f-23b222ff7612)
3) Create user:
![image](https://github.com/yaroslav-lysenko-axon/InventorSoftTest/assets/88324041/62b2f5cf-990b-4beb-b52d-74c4383e6070)
4) Get users:
![image](https://github.com/yaroslav-lysenko-axon/InventorSoftTest/assets/88324041/cea90efd-0df2-44b7-8d3f-02f061f6f4bf)

