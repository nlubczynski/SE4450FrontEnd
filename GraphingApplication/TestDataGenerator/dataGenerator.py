import MySQLdb
import time

while True:
        db = MySQLdb.connect(host="localhost",
                     user="mysqluser",
                     passwd="Powersmiths1",
                     db="powersmiths",
                     port=3307)

        cur = db.cursor()

        from random import randint

        random1 = randint(-1000, 1000)
        random2 = randint(-1000, 1000)

        command = "INSERT INTO SensorReading VALUES(null, '1', null, '"
        command += str(random1)
        command += "'), (null, '2', null, '"
        command += str(random2)
        command += "');"

        print command
        cur.execute(command)
        db.commit()

        time.sleep(5)

