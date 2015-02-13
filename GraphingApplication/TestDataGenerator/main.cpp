/* Standard C++ includes */
#include <stdlib.h>
#include <iostream>
#include <chrono>
#include <thread>
#include <sstream>

/*
Include directly the different
headers from cppconn/ and mysql_driver.h + mysql_util.h
(and mysql_connection.h). This will reduce your build time!
*/
#include "include/mysql_connection.h"

#include <cppconn/driver.h>
#include <cppconn/exception.h>
#include <cppconn/resultset.h>
#include <cppconn/statement.h>

using namespace std;

int main(void)
{
	std::cout << endl;
	std::cout << "Starting" << endl;

	for (;;)
	{
		// sleep for 5 seconds
		std::this_thread::sleep_for(std::chrono::milliseconds(5000));

		string ran1string, ran2string;

		try {
			sql::Driver *driver = nullptr;
			sql::Connection *con = nullptr;
			sql::Statement *stmt = nullptr;
			sql::ResultSet *res = nullptr;

			/* Create a connection */
			if (!driver)
				driver = get_driver_instance();

			con = driver->connect("tcp://127.0.0.1:3306", "root", "ERTWwesternmagnethorse");
			/* Connect to the MySQL test database */
			con->setSchema("powersmiths");

			stmt = con->createStatement();
			int ran1 = -50 + rand() % 100;
			int ran2 = -50 + rand() % 100;

			ran1string = to_string(ran1);
			ran2string = to_string(ran2);

			std::stringstream ss;
			ss << "INSERT INTO SensorReading VALUES (null, '1', null, '" << ran1string << "'), (null, '2', null, '" << ran2string <<"');" ;
			sql::SQLString command(ss.str().c_str());
			string test = ss.str().c_str();
			res = stmt->executeQuery(command);

			delete res; res = nullptr;
			delete stmt; stmt = nullptr;
			delete con; con = nullptr;

		}
		catch (sql::SQLException &e) 
		{
			if (e.getErrorCode() != 0)
			{
				std::cout << "# ERR: SQLException in " << __FILE__;
				std::cout << "(" << __FUNCTION__ << ") on line " << __LINE__ << endl;
				std::cout << "# ERR: " << e.what();
				std::cout << " (MySQL error code: " << e.getErrorCode();
				std::cout << ", SQLState: " << e.getSQLState() << " )" << endl;
				break;
			}
			else
			{
				std::cout << "Added values: " + ran1string + " and " + ran2string + ".\n";
			}
		}
	}

	std::cout << "Stoping...";

	return EXIT_SUCCESS;
}