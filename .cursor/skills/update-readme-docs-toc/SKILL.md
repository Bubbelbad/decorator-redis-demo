---
name: update-readme-docs-toc
description: Keeps README.md accurate and up-to-date with the current state of the repository. Use when the user adds new files, changes the tech stack, updates the project structure, refactors code, or asks to update, refresh, or sync the README.
---

# Update README

Keep `README.md` accurate as the repository evolves.

## When to apply

- New files, folders, or architectural layers are added/removed
- Dependencies or frameworks change
- The running/setup instructions change
- The user asks to update, refresh, or sync the README

## Workflow

1. **Scan the codebase** — read the top-level directory and relevant source folders to understand the current structure, key files, and tech used.
2. **Read `README.md`** — identify every section and note what is stale or missing.
3. **Update only what changed** — preserve the existing tone and format; do not rewrite sections that are still accurate.

## Sections to maintain

| Section | What to keep current |
|---|---|
| **What it does** | High-level description of the app's purpose and key components (interfaces, classes, patterns) |
| **Tech stack** | All frameworks, libraries, and tools actually used (check `.csproj`, `docker-compose.yml`, etc.) |
| **Running locally** | Steps, ports, and any prerequisites (e.g. secrets files) |
| **Project structure** | The directory tree — add new files/folders, remove deleted ones |

## Rules

- Reflect what is **actually in the code**, not what was planned.
- Do not invent sections unless the user asks.
- Keep the project-structure tree in sync with real files — read the source directories before updating it.
- If a dependency is removed, remove it from the tech stack table.
