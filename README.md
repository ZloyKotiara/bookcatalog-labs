# Лабораторные работы по ASP.NET Core + PostgreSQL

Проект закрывает сразу 4 лабораторные:

- ЛР №1 — CRUD-приложение на ASP.NET Core MVC + PostgreSQL через ORM (Entity Framework Core)
- ЛР №2 — контейнеризация через Docker / Docker Compose
- ЛР №3 — CI/CD через GitHub Actions + функциональный тест
- ЛР №4 — отдельное приложение для демонстрации round robin через Nginx

## Тематика приложения

Основное приложение — `Каталог книг`.

Сущность `Book`:
- Id
- Title
- Author
- Genre
- Year

## Структура

```text
bookcatalog-labs/
├── docker-compose.yml
├── .env.example
├── src/
│   └── BookCatalog.Web/
├── tests/
│   └── BookCatalog.Web.Tests/
├── .github/
│   └── workflows/
│       └── ci-cd.yml
└── load-balancer/
    ├── docker-compose.yml
    ├── nginx/
    │   └── default.conf
    └── src/
        └── NodeInfo.App/
```

## ЛР №1. Запуск локально без Docker

Требования:
- .NET 8 SDK
- PostgreSQL

1. Создайте БД в PostgreSQL.
2. Обновите строку подключения в `src/BookCatalog.Web/appsettings.json` либо задайте переменные окружения:
   - `DB_HOST`
   - `DB_PORT`
   - `DB_NAME`
   - `DB_USER`
   - `DB_PASSWORD`
3. Перейдите в каталог приложения:

```bash
cd src/BookCatalog.Web
```

4. Запустите приложение:

```bash
dotnet restore
dotnet run
```

Приложение само создаст таблицу через `EnsureCreated()` при старте.

## ЛР №2. Запуск через Docker Compose

1. Скопируйте `.env.example` в `.env`
2. При необходимости поменяйте значения.
3. В корне проекта выполните:

```bash
docker compose up --build
```

После запуска:
- приложение: `http://localhost:8080`
- healthcheck: `http://localhost:8080/health`

## ЛР №3. CI/CD

Workflow находится в файле:

```text
.github/workflows/ci-cd.yml
```

Что делает пайплайн:
- восстанавливает зависимости
- собирает проект
- запускает функциональный тест
- собирает Docker-образ
- публикует образ в GHCR при пуше в `main`

### Секреты GitHub

Добавьте в GitHub Secrets:
- `DB_NAME`
- `DB_USER`
- `DB_PASSWORD`
- `DB_HOST`
- `DB_PORT`

## ЛР №4. Балансировка Nginx (round robin)

Перейдите в каталог:

```bash
cd load-balancer
```

Запуск:

```bash
docker compose up --build
```

Откройте:

```text
http://localhost:8088
```

При обновлении страницы будут по очереди отображаться разные ноды:
- Нода 1
- Нода 2
- Нода 3

## Что показать на защите

### По ЛР №1
- главную страницу со списком книг
- создание, редактирование, просмотр и удаление записи
- PostgreSQL как основную БД
- `AppDbContext` и модель `Book`

### По ЛР №2
- `Dockerfile` с multi-stage
- `docker-compose.yml`
- `depends_on`
- переменные окружения
- `HEALTHCHECK`

### По ЛР №3
- workflow в GitHub Actions
- запуск теста `HomePage_or_BooksPage_should_open_successfully`
- использование GitHub Secrets

### По ЛР №4
- `nginx/default.conf`
- 3 экземпляра приложения `NodeInfo.App`
- демонстрация смены ноды при обновлении страницы
