# module "vpc" {
#   source  = "terraform-aws-modules/vpc/aws"
#   version = "5.19.0"

#   name = "microblogging_app"
  
#   cidr = "10.0.0.0/16"
#   azs             = ["us-east-2a", "us-east-2b", "us-east-2c"]

#   private_subnets = ["10.0.1.0/24", "10.0.2.0/24"]
#   public_subnets  = ["10.0.101.0/24", "10.0.102.0/24"]
#   map_public_ip_on_launch = true

#   enable_nat_gateway   = true
#   single_nat_gateway   = true
#   enable_dns_hostnames = true
#   enable_dns_support   = true

#   public_subnet_tags = {
#     "kubernetes.io/role/elb" = 1
#   }

#   private_subnet_tags = {
#     "kubernetes.io/role/internal-elb" = 1
#   }
# }

# resource "aws_security_group" "microblogging_app" {
#   name   = "microblogging_app"
#   vpc_id = module.vpc.vpc_id

#   tags = {
#     Name = "microblogging_app"
#   }
# }

# resource "aws_vpc_security_group_ingress_rule" "allow_postgres" {
#   security_group_id = aws_security_group.microblogging_app.id
#   description       = "Allow Postgres from specific IP"
#   from_port         = 5432
#   to_port           = 5432
#   ip_protocol       = "tcp"
#   cidr_ipv4         = "104.30.160.22/32"
# }


module "vpc" {
  source  = "terraform-aws-modules/vpc/aws"
  version = "5.8.1"

  name = "education-vpc"

  cidr = "10.0.0.0/16"
  azs  = slice(data.aws_availability_zones.available.names, 0, 3)

  private_subnets = ["10.0.1.0/24", "10.0.2.0/24", "10.0.3.0/24"]
  public_subnets  = ["10.0.4.0/24", "10.0.5.0/24", "10.0.6.0/24"]

  enable_nat_gateway   = true
  single_nat_gateway   = true
  enable_dns_hostnames = true

  public_subnet_tags = {
    "kubernetes.io/role/elb" = 1
  }

  private_subnet_tags = {
    "kubernetes.io/role/internal-elb" = 1
  }
}
