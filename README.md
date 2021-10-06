# Constellation
 This application is developed along with my students

# Casebeskrivelse
Som en vigtig del af uddannelsen Datatekniker med speciale i programmering, skal eleverne udvælge en række teknologier og bringe dem i samspil. Dette projekt kalder vi mini-svendeprøven, der er en optagt til den rigtige afsluttende svendeprøve. Der er flere krav til teknologivalg - der skal blandt være en app, en database, en webservice og embedded udstyr som en del af projektet. Et populært valg af App teknologi, blandt eleverne, er at bygge en PWA (Progressive web app), men den løsning har en mærkbar ulempe, da appen skal serveres over den sikre protokol HTTPS og enhver server-side interaktion skal også foregår over HTTPS. Eleverne har dog sjældent mulighed for at sikre kommunikation over HTTPS da det kræver et SSL certifikat. En løsning på problem var at tilbyde eleverne at hoste et webapi via skolens eller et lign netværk, hvor det er muligt at bruge HTTPS.

# Problemforumerling
Hvordan kan jeg tilbyde eleverne adgang og let adminstration af en docker container
