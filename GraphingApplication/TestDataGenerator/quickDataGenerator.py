from __future__ import division
import MySQLdb
import time

numberOfRows = int(input("Enter number of rows to add:"))

db = MySQLdb.connect(host="localhost",
                     user="mysqluser",
                     passwd="Powersmiths1",
                     db="powersmiths",
                     port=3307)

cur = db.cursor()


for x in range(0, numberOfRows):
        from random import randint

        random1 = randint(-1000, 1000)
        random2 = randint(-1000, 1000)

        command = "INSERT INTO SensorReading VALUES(null, '1', null, '"
        command += str(random1)
        command += "'), (null, '2', null, '"
        command += str(random2)
        command += "');"

        cur.execute(command)
        db.commit()

	if (x / numberOfRows) * 100  % 10 == 0:
		print "{}% ...".format( x * 100 / numberOfRows)

print "Done"


