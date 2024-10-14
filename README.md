# Prog5

Branches hebben lowercase namen.

![image](https://github.com/user-attachments/assets/88070fe3-0dc2-409a-8b82-aae87f7e4ab1)

# 🛒 Bumbo applicatie

<br />

## 📄 Inhoud
1. [Uitleg](#-uitleg)
2. [Trello](#-trello)
3. [Coding guidelines](#-coding-guidelines)
4. [Handige linkjes](#-handige-linkjes)

<br />

## 👀 Uitleg
### Branches

#### Develop
- Zitten alle afgeronde features

#### Staging
- Wanneer er getest wordt dan wordt de Develop branch overgezet naar Staging
- Hierin kan getest worden 

#### Hotfix
- Als er een fout gevonden wordt op staging kan deze via een losse Hotfix branch verbeterd worden en toegevoegd aan de staging
- De Staging wordt na een hotfix weer gemerged met develop

#### Main
- Wanneer de testen zijn geslaagd kan de Staging branch gemerged worden naar Master
- Met toegevoegd versie label
- Dit is de productie branch en zou altijd een complete en geteste versie moeten bevatten die werkt.

### Feature branch & Pull request
- Geef relevante namen aan je branch en pull request (PR)
- Koppel de branch en/of de pr aan de trello taak
- Pull request vereist goedkeuring van 2 reviewers voordat deze gemerged kan worden

### Mergen
- Altijd via een pull request
- Pas merge als PR is goedgekeurd (door min. 2 reviewers)
- Feature branches moeten altijd naar develop worden gemerged
- Zorg dat de nieuwste versie van develop in je branch zit (zonder conflicts ;) )


## 🧾 Coding guidelines

### Best practices
Voor beste performance en betrouwbaarheid
- [Microsoft best practices](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/best-practices?view=aspnetcore-8.0)


### Naming conventions
Voor leesbaarheid en consistentie
- [Microsoft naming conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names)
- [Overzichtelijke versie](https://github.com/ktaranov/naming-convention/blob/master/C%23%20Coding%20Standards%20and%20Naming%20Conventions.md)


### Code conventions
Voor leesbaarheid, consistentie en werkbaar voor team
- [Coding conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)


- [.NETCore docs](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0)










