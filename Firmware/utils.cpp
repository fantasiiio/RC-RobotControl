#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>

char *strdup (const char *s) {
    char *d = (char*)malloc (strlen (s) + 1);   // Allocate memory
    if (d != NULL) strcpy (d,s);         // Copy string if okay
    return d;                            // Return new memory
}

char** str_split(char* a_str, const char a_delim, int &count)
{
    char** result    = 0;
    //size_t count     = 0;
    char* tmp        = a_str;
    char* last_comma = 0;
    char delim[2];
    delim[0] = a_delim;
    delim[1] = 0;

	count = 0;
    /* Count how many elements will be extracted. */
    while (*tmp)
    {
        if (a_delim == *tmp)
        {
            count++;
            last_comma = tmp;
        }
        tmp++;
    }

    /* Add space for trailing token. */
    count += last_comma < (a_str + strlen(a_str) - 1);

    /* Add space for terminating null string so caller
       knows where the list of returned strings ends. */
    //count++;

    result = (char**)malloc(sizeof(char*) * count);

    if (result)
    {
        int idx  = 0;
        char* token = strtok(a_str, delim);

        while (token)
        {
            //assert(idx < count);
            *(result + idx++) = strdup(token);
            token = strtok(0, delim);
        }
        //assert(idx == count - 1);
        *(result + idx) = 0;
    }

    return result;
}

float GetTriangleAngle2(float a, float b, float c)
{
	return (float)(acos( (b * b + c * c - a * a) / (2.0f * b * c)));
}

