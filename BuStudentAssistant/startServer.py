import os
os.system("clear")
os.system("net stop MongoDB")
print("--- Starting MongoDB Server ---")
Project_file_Path = os.getcwd() + "/mongoDB"
os.system("mongod --dbpath \"{}\"".format(Project_file_Path))

