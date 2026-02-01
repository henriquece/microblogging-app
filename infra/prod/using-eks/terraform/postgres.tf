# resource "aws_db_instance" "social_media" {
#   engine            = "postgres"
#   allocated_storage = 10
#   instance_class    = "db.t3.micro"

#   username = "testt"
#   password = "12345678"

#   skip_final_snapshot = true

#   publicly_accessible    = true
#   db_subnet_group_name   = aws_db_subnet_group.social_media.name
#   vpc_security_group_ids = [aws_security_group.social_media.id]
# }

# resource "aws_db_subnet_group" "social_media" {
#   name       = "social-media-rds-subnet-group"
#   subnet_ids = module.vpc.public_subnets
# }