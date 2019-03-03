# WebLoadTestTool
Load Test for Website

1.Define the testing URL in the WebURL.txt file.
The settings format is as follow:
Job Name      URL                               Request Method
Demo1         https://google.com                Get

[Job Name] is used for linkup the testing URL with testing parameter(optional).
[URL] is the URL of the target web service / web API.
[Request Method] is the HTTP request method of the web service / web API.

2.Define the testing parameter data in the /Data/ Folder
Create a .txt file in this folder and the file name should be same as the [Job Name].

For example:
Create Demo1.txt if you want to past the testing parameter to the URL.

The content of the parameter is in JSON format and the program will convert it and pass the parameter value when the HTTP request is sent.
The parameter format is as follow:
{Demo1_Parameter01": "value1", Demo1_Parameter02: "value2"}

The program will sent the web request : https://google.com?Demo1_Parameter01=value1&Demo1_Parameter02=value2

3.Run the window program for load test
[No. of Virtual User] is the total no. of virtual user to test the URL.

[User per Iteration] is the amount of user for each testing iteration. 
For example, if the [No of Virtual User] is 100 and [User per Iteration] is 10, the program will automatically divide 100 users into 10 testing iteration and execute it one by one.

[Start after  Sec(s)] Each testing iteration can set a timer to defer the start time. 
Regarding to above example, if you set 2 seconds then each testing iteration will wait for 2 seconds before execution. 
Therefore, the first iteration will wait for 2 seconds and the second iteration will wait for 4 seconds ( 2 + 2) ......etc.

[Repeat Task] Check this option to enable the Virtual user to repeatly sent the HTTP request.

[Thread sleep time of each thread (sec)] Once the [Repeat Task] is checked, you need to input the thread sleep time between each repeating task.

[No. of user for each iteration task] to define the no. of user for each iteration testing task. 
For example, if you have set 2 URL for testing, that means there are 2 tasks in each iteration.
So you need to set the user amount for each task, say 5/5, 5 for the first task and 5 for the second task.

[Start] Click this to start the load test process.

[Stop] Click this to stop the load test process.

4.Report
After clicking the [Stop] button, a simple load test report will be generated automatically.
