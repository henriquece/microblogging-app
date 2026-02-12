# resource "aws_db_instance" "microblogging_app" {
#   engine            = "postgres"
#   allocated_storage = 10
#   instance_class    = "db.t3.micro"

#   username = "testt"
#   password = "12345678"

#   skip_final_snapshot = true

#   publicly_accessible    = true
#   db_subnet_group_name   = aws_db_subnet_group.microblogging_app.name
#   vpc_security_group_ids = [aws_security_group.microblogging_app.id]
# }

# resource "aws_db_subnet_group" "microblogging_app" {
#   name       = "microblogging-app-rds-subnet-group"
#   subnet_ids = module.vpc.public_subnets
# }