# Makefile for Docker .NET Clean Build

COMPOSE = docker-compose -f docker-compose.dev.yml

SOLUTION = Users.sln

.PHONY: analyze roslynator format

analyze: roslynator format

roslynator:
	roslynator analyze $(SOLUTION)

format:
	dotnet format --verify-no-changes 