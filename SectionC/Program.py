# Due to the pandemic, the company experiences a drastic drop in its sales and revenue. 
# It has decided to deduct the salary of full-time employee who joined after the year of 1995 by 15%
# Calculate new salary of the affected employees and print out the total of the computed salary.

from functools import reduce

employees = []
with open("HRMasterlist.txt",'r') as file:
    for line in file:
        employees.append(line.split('|'))
file.close()

total_new_salary = reduce(lambda x,y:x + y,map(lambda i:int(i[8]) * 0.85,filter(lambda i:i[7] == "FullTime" and int(i[3][6::]) > 1995,employees)))
print("Total amount of salary paid to the affected employees after deduction: $",total_new_salary)