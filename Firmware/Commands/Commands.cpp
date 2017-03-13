#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "Commands.h"
#include "utils.h"

void CommandList::parseHelp(CommandArgs cmdArgs)
{
}

void CommandList::init(int maxCommands)
{
    this->maxCommands = maxCommands;
    commands = new Command[maxCommands];
}

CommandList::~CommandList()
{
    delete commands;
}

CommandList::CommandList()
{
    this->init(12);
}

void CommandList::printCommandList()
{
	printf("\r\nCommand List:\r\n");
	for(int i = 0; i < this->commandCount; i++)
	{
		printf("%s\r\n", this->commands[i].commandName);
	}
}

CommandList::CommandList(int maxCommands)
{
    this->init(maxCommands);
}

void CommandList::add(const char * commandName, void(*cmdCallback)(CommandArgs commandArgs))
{
    if(commandCount >= maxCommands)
        return;

    commands[commandCount].commandName = commandName;
    commands[commandCount].cmdCallback = cmdCallback;
    commandCount++;
}

void (*CommandList::Callback(const char* commandName))(CommandArgs commandArgs)
{
    for(int i = 0; i < this->commandCount; i++) {
        if(strcmp(this->commands[i].commandName, commandName) == 0) {
            return this->commands[i].cmdCallback;
        }
    }
    return NULL;
}

void CommandList::parseCommand(char* command)
{
    char** tokens;
    CommandArgs commandArgs;
    
    void(*cmdCallback)(CommandArgs commandArgs);
    int tokenCount;

    tokens = str_split(command, ' ', tokenCount);

    if (tokens) {
        cmdCallback = this->Callback(tokens[0]);
        if(cmdCallback) {
            commandArgs.params = tokens;
            commandArgs.paramCount = tokenCount;
            cmdCallback(commandArgs);
        }
        else // unknown function
        {
	        cmdCallback = this->Callback("unknown");
	        if(cmdCallback) {
	            commandArgs.params = tokens;
	            commandArgs.paramCount = tokenCount;
	            cmdCallback(commandArgs);
	        }
        	
        }
        for(int i = 0; i < tokenCount; i++)
            free(tokens[i]);
        free(tokens);
    }
}
