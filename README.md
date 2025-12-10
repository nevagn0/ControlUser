Запуск контейнер ```docker-compose up -d``` из дирректории ```*\ControlSystemOrders```   
Использованные версии:   
  Dotnet 9.0.0   
  Microsoft.AspNetCore.Authentication.JwtBearer 9.0.0  
  Microsoft.AspNetCore.OpenApi 9.0.9  
  Microsoft.EntityFrameworkCore 9.0.0  
  Microsoft.EntityFrameworkCore.Design 9.0.0-rc1.24451.1  
  Npgsql.EntityFrameworkCore.PostgreSQL 9.0.0-rc.1  
  Swashbuckle.AspNetCore.SwaggerGen 9.0.0  
  Swashbuckle.AspNetCore.SwaggerUI 9.0.0  
  MassTransit.RabbitMQ 9.0.9=0-devolp.27  
  Yarp.ReverseProxy 2.3.0  
  
Доступные endpoint для UserServices:  
  Регистрация: ```/api/User/register```,  
  Авторизация с получением токена: ```/api/User/login```,  
  Выход с удалением токена: ```/api/User/logout```,  
  Получение всех пользователей только с правами: ```/api/User/GetAllUsers```;  
