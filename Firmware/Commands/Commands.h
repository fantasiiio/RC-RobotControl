class CommandArgs
{
public:
	int paramCount;
	char **params;	
};

class Command
{
public:
	const char *commandName;
    void(*cmdCallback)(CommandArgs commandArgs);
};

class CommandList
{
private:
	int maxCommands;
	Command *commands;
	int commandCount;
	void init(int maxCommands);


public:
	CommandList();
	~CommandList();
	CommandList(int maxCommands);
	void add(const char * commandName, void(*cmdCallback)(CommandArgs commandArgs));
	void (*Callback(const char* commandName))(CommandArgs commandArgs);
	void clear();
	void parseCommand(char* command);
	void parseHelp(CommandArgs cmdArgs);
	void printCommandList();
};
