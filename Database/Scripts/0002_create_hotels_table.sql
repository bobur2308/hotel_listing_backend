create table hotels(
                       id bigserial primary key ,
                       full_name varchar(255) not null ,
                       address varchar(255) ,
                       rating float ,
                       country_id bigint not null
)
