select top 3 name, department, sum(net_salary) as total_net
from Employees e
join Payroll p on e.employee_id = p.employee_id
where pay_date >= '2024-01-01' and pay_date <= '2024-12-31'
group by name, department
order by total_net desc

----------------------------------------------

select department, sum(gross_salary) as total_gross, sum(tax_amount) as tax_deducted, avg(net_salary) as average_net
from employees e
join payroll p on e.employee_id = p.employee_id
group by department

----------------------------------------------

select name
from employees e
where not exists (
    select 1 from payroll p 
    where p.employee_id = e.employee_id 
    and pay_date >= '2024-01-01' and pay_date <= '2024-12-31'
)

----------------------------------------------

with recentpayandnet as (
    select employee_id, pay_date, net_salary, row_number() over (partition by employee_id order by pay_date desc) as row_num
    from payroll
)
select name, pay_date, net_salary
from employees e
join recentpayandnet r on e.employee_id = r.employee_id
where r.row_num = 1

----------------------------------------------

--pay_date and employee_id
--pay_date because it will always be used when filtering and employee_id so it will still be faster when used in grouping by and joining within the pay_date range

----------------------------------------------

--because it is non sargable which is something the db must do to perform indexing, when you put transforming data in the where clause like year() it will prevent indexing so it will do a table scan which is slower so instead of doing where year(pay_date) = 2024 you can do where pay_date >= '2024-01-01' AND pay_date <= '2024-12-31' because the db can still do indexing using the pay_date to compare for a range

----------------------------------------------

select rank() over (partition by department order by sum(net_salary) desc) as salary_rank, department, name, sum(net_salary) as total_net
from employees e
join payroll p on e.employee_id = p.employee_id
group by department, name