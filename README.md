# AcmeCorporationWebApp
A Web application project, for a Umbraco Job Interview

After you have cloned this repository, you need to make sure to run all the sql queries, that will create the tables and stored procedures.
These are needed for the application to work. The application code in the data access class, has a connectstring for (localDB)v11.0.
If you like to use another database, you just need to change the connection string at the top of the class.
The first time you run the code, the application will check, if there is a file, in the "MyDocuments" folder, called psn.txt, if not
It will create 100 GUIDs, create the file and write the guids into the file. This file is used to generate the ProductSerialNumber objects,
which is stored in the database. It will also check if the database already have these PSN's stored, before trying to do so.

When the application is running, you will have a front page, where you can choose to go, to "Form" or "Submissions".
Form is where you can give all the info with a PSN, and see if the key is still valid. The key can be used 2 times, and after 2 uses
the key will be invalid.
In submissions you can see all the submissions, that people have submitted via the form.
