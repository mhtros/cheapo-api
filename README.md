#  <img src="https://github.com/mhtros/cheapo-api/blob/main/Cheapo.Api/wwwroot/logo192.png" width="30px" /> Cheapo-api

 A .NET 6 WEB API application to keep track your monthly expenses.

![OS](https://img.shields.io/badge/OS-cross_platform-lightgrey) ![Framework](https://img.shields.io/badge/framework-.Net%206-green)

Online Demo: <a href="https://chepo-tracker.herokuapp.com/">Heroku demo app</a>

## Features

- Sign in / Sign up
- Email confirmation
- Forgot Password
- Two Factor authentication
- Insert transaction (incomes or expenses)
- Compare transaction given a date range
- Multiple staticts charts based on a date range

## Configurations

In order the application to work you must declare the following properties into the secrets.json file

- IssuerSigningKey: The string that will be used as the key to create the token signature. Used as argument in the hash function.
- Issuer: Τhe server that issued the token.
- Audience: Which server can accept the token.
- ValidateIssuer: If <b>true</b> the issuer will be validated during token validation.
- ValidateAudience: If <b>true</b> the audience will be validated during token validation.
- ValidateLifetime: If <b>true</b> the lifetime of the token (is it expired or valid) will be validated during token validation.
- RequireExpirationTime: Gets or sets a value indicating whether tokens must have an 'expiration' value.
- ValidateIssuerSigningKey: If <b>true</b> the issuer signin key will be validated during token validation.
- From: Τhe email that the application will use to send other emails.
- SmtpServer: The server that will forward the emails.
- Port: SMPT port number that will used to forward the emails.
- Username: Sender email (used for authentication purposes).
- Password: The email password (used for authentication purposes).
