# Proyecto: Identity Server.

## Tabla de Contenidos
1. [Introducción](#introducción)
2. [Flujo de Trabajo con Git](#flujo-de-trabajo-con-git)
   - [Creación y Gestión de Ramas](#creación-y-gestión-de-ramas)
   - [Estándares para Mensajes de Commits](#estándares-para-mensajes-de-commits)
   - [Comandos Git Frecuentes](#comandos-git-frecuentes)
3. [Estándares de Código](#estándares-de-código)
4. [Recomendaciones Generales](#recomendaciones-generales)

## Introducción

Este documento proporciona una guía clara para el equipo de desarrollo sobre cómo gestionar el flujo de trabajo, adherirse a los estándares de código, y seguir las mejores prácticas durante el desarrollo de [Nombre del Proyecto].

## Flujo de Trabajo con Git

### Creación y Gestión de Ramas

- **Trabaja en ramas específicas para cada funcionalidad**:
  - Crea una nueva rama para cada funcionalidad o tarea en la que trabajes. Usa nombres descriptivos y sigue una convención clara.
  - **Ejemplo**: `feature/sobre-nosotros`, `fix/bug-visualizacion-perfil`.
- **No acumules múltiples funcionalidades en una misma rama**:
  - Una vez que termines una funcionalidad, fusiona la rama con `main` o `develop` a través de un pull request, y crea una nueva rama para la siguiente tarea.
- **Nombra las ramas consistentemente**:
  - Usa prefijos como `feature/`, `fix/`, `improvement/` para indicar el tipo de trabajo que se está realizando.
- **Mantén las ramas sincronizadas**:
  - Regularmente sincroniza tu rama con `main` o `develop` para minimizar conflictos al fusionar.
- **Revisa y elimina ramas inactivas**:
  - Elimina las ramas que ya se han fusionado para mantener el repositorio limpio.

### Estándares para Mensajes de Commits

- **Usa prefijos claros en los mensajes de commit**:
  - `feat`: Para nuevas funcionalidades.
  - `fix`: Para corrección de bugs.
  - `docs`: Para cambios en la documentación.
  - `style`: Para cambios de formato.
  - `refactor`: Para refactorización sin agregar ni corregir funcionalidades.
  - `test`: Para añadir o modificar tests.
  - `chore`: Para tareas de mantenimiento.
- **Estructura del mensaje de commit**:
  - **Título breve**: 50 caracteres o menos, en presente y sin punto final.
  - **Descripción opcional**: Explica qué y por qué se hicieron los cambios. Incluye referencias a issues o PRs.

### Comandos Git Frecuentes

- **`git init`**: Inicializar un nuevo repositorio.
- **`git clone <url>`**: Clonar un repositorio remoto.
- **`git status`**: Ver el estado actual del repositorio.
- **`git add <archivo>`**: Añadir archivos al área de preparación.
- **`git commit -m "mensaje"`**: Crear un commit con un mensaje corto.
- **`git push`**: Enviar commits al repositorio remoto.
- **`git pull`**: Traer los cambios del repositorio remoto al repositorio local.
- **`git branch`**: Listar, crear o cambiar ramas.
