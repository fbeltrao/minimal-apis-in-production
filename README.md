# Using minimal apis in production

[![Build and test](https://github.com/fbeltrao/minimal-apis-in-production/actions/workflows/build_and_test.yml/badge.svg)](https://github.com/fbeltrao/minimal-apis-in-production/actions/workflows/build_and_test.yml)

This repository evaluates using ASP.NET Core minimal APIs in production in an opinionated fashion.
The definitions here are based on practice building and APIs with .NET, and don't fulfill all requirements. Think of it as an starting point.

## Requirements

### Abstraction / Decoupling

- [ ] Model validation using FluentValidation
- [ ] Business logic is clean from ASP.NET or API specific details

### Response customization

- [ ] Correlation-Id in response headers
- [ ] Api version in response headers
- [ ] Common response model for errors (application, validation errors)
- [ ] Application logic errors are translated to HTTP status codes with and common payload

### Operationalization

- [ ] Logs, metrics and traces are collected

### Discoverabily

- [ ] API is discoverable with documentation
- [ ] Documentation is provided by code

### Testability

- [ ] Easy to write unit tests with mocking libraries
- [ ] Integration Tests to validate API requests and responses