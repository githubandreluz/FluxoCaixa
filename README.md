# FluxoCaixa
Challenge sent by Amaris


### Application “Fluxo de Caixa” (Cash Flow)
This application was developed as a challenge exercise proposed by Amaris. 
Objective: 
	Receive the cash flow information and consolidate the balance per day.
	
	
I chose .Net WebApi and C# to develop this challenge because it is the language that I have most skills.

I used DDD in order to provide a best presentation and organization of my code.

In order of keep things simple I created a context with three lists simulating database tables, this context is injected as “singleton” in order to keep data while the application is running.

I also included a hosted service that runs every 3 seconds looking for new values that wasn't consolidated yet, only dates with pendent status are recalculated.
This application also has Unit Tests.

You can access the API swagger running the project, it should be accessed by this address https://localhost:7128/swagger/index.html

At the root folder of this project you can find a Dockerfile properly configured and ready to be used with docker, I will approach this topic later.

### About Authentication and Authorization
For this WebApi was implemented Authentication and Authorization, only registered users can access the endpoints and only users with role “Admin” can save a new credit or debit registry.

There are two different possible roles in this project “Admin” and “Funcionario” (employee), users with Role Admin can access everything, but users with role “Funcionario” can only access the consolidated report.

User with role “Admin”:
	Login: andre
	Password: andrepassword

User with role “Funcionario”
	Login: joao
	Password: joaopassword


### How to test the application: 

You can clearly test the WebApi just running the application through Visual Studio and use Postman to test the endpoints, but I’m choosing do it via curl commands and using the application running on Docker.

I’m using a Docker container, with Windows 10 and PowerShell for terminal.
Let’s follow the steps below: 

#### 1 – Creating the container:

	1.1	Open PowerShell on windows.

	1.2	Navigate to application root path where is located the DockerFile

		Example: 
`cd “D:\teste\fluxo de caixa (amaris)\FluxoDeCaixa.API”`

	1.3	Type the following command: 

`docker build -t fluxo-de-caixa .`

	1.4	It Should look like this image:

![Test Image 1](./readmeImg/Img-001.PNG)

#### 2	– Running the container and configuring the port 5001 in order to use it in our curl commands 

	2.1	Type the following command: 
`docker run -ti -d -p 5001:80 fluxo-de-caixa`
 ![Test Image 2](./readmeImg/Img-002.PNG)

#### 3-	Now our container is created and already running, lets try to authenticate firstly with the user “andre”
Type the following command for that:

`curl.exe --location --request POST 'http://localhost:5001/api/v1/Auth/Login' --header 'Content-Type: application/json' --data-raw '{\"login\": \"andre\",\"password\": \"andrepassword\"}'`


The application will return to us the information for an authenticated user just like this:
![Test Image 3](./readmeImg/Img-003.PNG) 

Let’s copy the token created during our authentication, and try to save a new credit, just reminding that you need to create your own token and replace it in the command line below:

`curl.exe --location --request POST 'http://localhost:5001/api/v1/Lancamentos/CriarLancamento' --header 'Authorization: Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImFuZHJlIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFuZHLDqSBMdXoiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiJhZTkyZjUyYy1jNTI0LTRmMGYtOWY0YS1mY2UwYzkxNDNlOWYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTY2ODEwMDI1OCwiaXNzIjoiU2Vydmlkb3JGbHV4b0RlQ2FpeGEifQ.Hn1JlJhOI8-wmyBk4L_yVBbza6EO8xopqPsM7Apvs7s' --header 'Content-Type: application/json' --data-raw '{\"idTipoLancamento\": 1,\"valor\": 23,\"dataLancamento\": \"2022-11-07\"}'`
	This endpoint doesn’t return any information in case of success, it only returns HttpCode = 200.

### 4-	Let’s also create a debit using this same token, it will take one hour until it expires

`curl.exe --location --request POST 'http://localhost:5001/api/v1/Lancamentos/CriarLancamento' --header 'Authorization: Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImFuZHJlIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFuZHLDqSBMdXoiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiJhZTkyZjUyYy1jNTI0LTRmMGYtOWY0YS1mY2UwYzkxNDNlOWYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTY2ODEwMDI1OCwiaXNzIjoiU2Vydmlkb3JGbHV4b0RlQ2FpeGEifQ.Hn1JlJhOI8-wmyBk4L_yVBbza6EO8xopqPsM7Apvs7s' --header 'Content-Type: application/json' --data-raw '{\"idTipoLancamento\": 2,\"valor\": 10,\"dataLancamento\": \"2022-11-07\"}'`

**Note:** In order to create a new Debit, it´s necessary change the value of the field “idTipoLancamento” from 1 to 2.

After those steps above we should have one credit of $23 and one debit of $10 for the same day, so the balance of the day should be

Your terminal should be like this: 
 ![Test Image 4](./readmeImg/Img-004.PNG) 
 
 
#### 5-	Now let’s check if our balance was correctly calculated, for that there is two endpoints that can be used,  ‘../api/v1/ConsultaConsolidado/ConsultarConsolidadoPorAno/2022' will filter only per year, showing all dates consolidated at this year, and you can also use ‘’ ../api/v1/ConsultaConsolidado/ConsultarConsolidadoPorAnoMes/2022/11’ where 2022 means the year and 11 means the month, so in this last endpoint you will get only the dates of the respective month.
Type the following command to see the balance of the whole year

`curl.exe --location --request GET 'http://localhost:5001/api/v1/ConsultaConsolidado/ConsultarConsolidadoPorAno/2022' --header 'Authorization: Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImFuZHJlIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFuZHLDqSBMdXoiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiJhZTkyZjUyYy1jNTI0LTRmMGYtOWY0YS1mY2UwYzkxNDNlOWYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTY2ODEwMDI1OCwiaXNzIjoiU2Vydmlkb3JGbHV4b0RlQ2FpeGEifQ.Hn1JlJhOI8-wmyBk4L_yVBbza6EO8xopqPsM7Apvs7s'`

That’s our balance for the whole year, and as expected the balance between $23 (credit) and $10 (debit) is $13
 ![Test Image 5](./readmeImg/Img-005.PNG) 


#### 6 – Now let’s try to create a new credit with Joao’s user, in order to test the authorization, Joao has the role “Funcionario” which should not be able to create new registries.

Firstly, we should make que authentication again in order to obtain a new token for joao.

Type the following code to retrieve Joao’s token:

`curl.exe --location --request POST 'http://localhost:5001/api/v1/Auth/Login' --header 'Content-Type: application/json' --data-raw '{\"login\": \"joao\",\"password\": \"joaopassword\"}'`

That’s the new token for Joao: 
 ![Test Image 7](./readmeImg/Img-007.PNG) 

Now, let’s try to create a new credit with this token.

Replace the token in this command line with the new token (Joao’s token), but this time we will add “-v” argument to the command line in order to be able to see the bad request returning with 403 - Forbidden:

`curl.exe -v --location POST 'http://localhost:5001/api/v1/Lancamentos/CriarLancamento' --header 'Authorization: Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImpvYW8iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSm_Do28gZGEgU2lsdmEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiI0ODEzNTg0NS0zY2Y3LTQ5NDItYWY1Ni1kMjFhYmMyZTVjMTUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJGdW5jaW9uYXJpbyIsImV4cCI6MTY2ODEwMjQ3NSwiaXNzIjoiU2Vydmlkb3JGbHV4b0RlQ2FpeGEifQ.0YHE7-7m4qM6iyqRk_qUAlBLHTU8ytbgHDTxXi-acT4' --header 'Content-Type: application/json' --data-raw '{\"idTipoLancamento\": 1,\"valor\": 23,\"dataLancamento\": \"2022-11-07\"}'`

![Test Image 8](./readmeImg/Img-008.PNG) 
 
