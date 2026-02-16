# 📊 KPI Pulse

<p align="center">
  <img src=".\src\KPI.Pulse.UI\Assets\KPI-logo.ico" alt="KPI Pulse Logo" width="200"/>
</p>

<p align="center">
  <strong>Легковесный монитор ключевых показателей эффективности для десктопа</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-purple" alt=".NET 8"/>
  <img src="https://img.shields.io/badge/Avalonia-11.0-blue" alt="Avalonia 11"/>
  <img src="https://img.shields.io/badge/platform-Windows%20%7C%20Linux%20%7C%20macOS-lightgrey" alt="Platforms"/>
  <img src="https://img.shields.io/badge/license-MIT-green" alt="License"/>
</p>

---

## 📋 О проекте

**KPI Pulse** — это учебно-демонстрационный проект, созданный для визуализации концепции мониторинга бизнес-показателей. Приложение позволяет в реальном времени отслеживать ключевые метрики, настраивать пороговые значения и получать уведомления при их достижении.

Проект разработан с использованием **Avalonia UI** — кроссплатформенного фреймворка, позволяющего запускать одно и то же приложение на Windows, Linux и macOS без изменения кода.

### 🎯 Цели проекта

- Демонстрация возможностей Avalonia UI для создания десктоп-приложений
- Пример реализации MVVM-архитектуры
- Показ работы с реактивными данными
- Создание кастомных контролов и стилей
- Демонстрация кроссплатформенной сборки

---

## ✨ Возможности

| | Функция | Описание |
|---|---|---|
| 📈 | **Дашборд** | Отображение ключевых KPI в реальном времени с автоматическим обновлением |
| 📊 | **Графики** | Визуализация динамики показателей |
| ⚙️ | **Настройки** | Конфигурация отслеживаемых показателей и пороговых значений |
| 💾 | **Сохранение** | Хранение настроек в JSON-файле |
| 🎨 | **Темизация** | Поддержка светлой и темной темы |

---

## 🖼️ Скриншоты

<p align="center">
  <img src="screenshots/dashboard.png" alt="Дашборд" width="700"/>
  <br/>
  <em>Главный экран с KPI-карточками</em>
</p>

<p align="center">
  <img src="screenshots/settings.png" alt="Настройки" width="700"/>
  <br/>
  <em>Экран настройки мониторов</em>
</p>

---

## 🚀 Технологический стек

- **[.NET 8](https://dotnet.microsoft.com/)** — современная кроссплатформенная платформа
- **[Avalonia UI](https://avaloniaui.net/)** — кроссплатформенный UI-фреймворк
- **MVVM** — архитектурный паттерн для разделения логики и представления
- **JSON** — хранение настроек и конфигураций

### Зависимости

```xml
<PackageReference Include="Avalonia" Version="11.0.0" />
<PackageReference Include="Avalonia.Desktop" Version="11.0.0" />
<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="8.0.0" />
<PackageReference Include="System.Text.Json" Version="8.0.0" />