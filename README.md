Tecnologias utilizadas no projeto

<img src="https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vue.js&logoColor=4FC08D"><img src="https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white"><img src="https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white"><img src="https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white"><img src="https://img.shields.io/badge/Microsoft_SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white">

<h1>Caixa Fácil</h1>

Simulando um sistema de caixa.

Crud simples e inserção de dados em massa com vue e aspnet core api

## Requisitos

1. Node 
2. Visual studio code ou visual studio
3. Sql Server 2008 ou superior


<h2>Como usar?</h2>

1. Executar os comandos para restaurar os pacotes: 

   <code>npm i </code>

   <code>dotnet build ou dotnet run</code>

   <code>dotnet ef database update</code> utilizar depois do passo **2** com o banco configurado

2. Alterar o arquivo appsettings.json e mudar o Context para suas configurações de banco de dados

   ```json
   "ConnectionStrings": { "Context":"Server=localhost;Database=nomedobanco;Trusted_Connection=True;MultipleActiveResultSets=true;User ID=nomedeusuario;Password=senhadobanco;Integrated Security=False"
     }
   ```

   

<h3>Pré visualização</h3>

<img src="https://github.com/alex-silveira/caixa-facil/blob/master/img/preview.png?raw=true">