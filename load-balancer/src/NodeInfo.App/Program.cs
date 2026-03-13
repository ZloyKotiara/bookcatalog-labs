var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    var nodeName = Environment.GetEnvironmentVariable("NODE_NAME") ?? "Неизвестная нода";
    var host = Environment.MachineName;

    var html = $$"""
<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>{{nodeName}}</title>
    <style>
        body {
            margin: 0;
            font-family: Arial, sans-serif;
            display: grid;
            place-items: center;
            min-height: 100vh;
            background: linear-gradient(135deg, #dbeafe, #eff6ff);
        }
        .card {
            background: white;
            padding: 40px;
            border-radius: 20px;
            box-shadow: 0 20px 40px rgba(0,0,0,0.12);
            text-align: center;
            min-width: 340px;
        }
        h1 {
            margin: 0 0 12px 0;
            color: #1d4ed8;
            font-size: 42px;
        }
        p {
            margin: 8px 0;
            color: #374151;
            font-size: 18px;
        }
    </style>
</head>
<body>
    <div class="card">
        <h1>{{nodeName}}</h1>
        <p>Round robin через Nginx</p>
        <p>Host: {{host}}</p>
    </div>
</body>
</html>
""";

    return Results.Content(html, "text/html; charset=utf-8");
});

app.Run();
