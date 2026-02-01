module "vpc" {
  source  = "terraform-aws-modules/vpc/aws"
  version = "5.19.0"

  name = "microblogging_app"
  cidr = "10.0.0.0/16"

  azs             = ["us-east-2a", "us-east-2b", "us-east-2c"]
  private_subnets = ["10.0.1.0/24", "10.0.2.0/24"]
  public_subnets  = ["10.0.101.0/24"]

  enable_dns_support   = true
  enable_dns_hostnames = true
}

resource "aws_security_group" "microblogging_app" {
  name        = "microblogging_app"
  description = "Allow SSH"
  vpc_id      = module.vpc.vpc_id

  tags = {
    Name = "microblogging_app"
  }
}

resource "aws_vpc_security_group_ingress_rule" "allow_ssh" {
  security_group_id = aws_security_group.microblogging_app.id
  description       = "Allow SSH from specific IP"
  from_port         = 22
  to_port           = 22
  ip_protocol       = "tcp"
  cidr_ipv4         = "104.30.160.22/32"
}

resource "aws_vpc_security_group_ingress_rule" "allow_postgres" {
  security_group_id = aws_security_group.microblogging_app.id
  description       = "Allow Postgres from specific IP"
  from_port         = 5432
  to_port           = 5432
  ip_protocol       = "tcp"
  cidr_ipv4         = "104.30.160.22/32"
}

resource "aws_vpc_security_group_ingress_rule" "allow_access" {
  security_group_id = aws_security_group.microblogging_app.id
  description       = "Allow access from specific IP"
  from_port         = 8080
  to_port           = 8080
  ip_protocol       = "tcp"
  cidr_ipv4         = "104.30.160.22/32"
}

resource "aws_vpc_security_group_ingress_rule" "allow_http" {
  security_group_id = aws_security_group.microblogging_app.id
  description       = "Allow http from specific IP"
  from_port         = 80
  to_port           = 80
  ip_protocol       = "tcp"
  cidr_ipv4   = "0.0.0.0/0"
}

resource "aws_vpc_security_group_ingress_rule" "allow_https" {
  security_group_id = aws_security_group.microblogging_app.id
  description       = "Allow https from specific IP"
  from_port         = 443
  to_port           = 443
  ip_protocol       = "tcp"
  cidr_ipv4   = "0.0.0.0/0"
}

resource "aws_vpc_security_group_egress_rule" "allow_all_outbound" {
  security_group_id = aws_security_group.microblogging_app.id

  cidr_ipv4   = "0.0.0.0/0"
  ip_protocol = -1
}