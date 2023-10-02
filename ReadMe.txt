Steps to load the code in Google Cloud:
	1. Cloud SQL - Create SQL instance - Migrate the tables and data .
	2. VPC Network - Create Serverless VPC connector with IP address range - 10.8.0.0/28
	3. Open cloud shell and run these commands( First one to set the project so that all instances of the project can talk to each other using VPC)
		a, gcloud config set project red-dominion-355018
		b, gcloud sql connect mysqlkenfrainstance --user=Kenfradbuser.
	4. In the c# connection string code, use the private IP address of the SQL instance created in step 1.
	5. Cloud Build - Build the code from github and appropriate branch.
	6. Cloud Service - Create an Instanc with the build from previous step, under connections, choose VPC Connector and Deploy.