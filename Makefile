banco:
	@echo Inicializando o Banco Estrela...
	@cd .\BancoEstrela\ && dotnet run
tests:
	@echo
	@cd BancoEstrelaTests && dotnet test