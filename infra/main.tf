provider "aws" {
  region = "us-east-2"
}

data "aws_ami" "ubuntu" {
  most_recent = true

  filter {
    name   = "name"
    values = ["ubuntu/images/hvm-ssd-gp3/ubuntu-noble-24.04-amd64-server-*"]
  }

  owners = ["099720109477"] # Canonical
}

resource "aws_key_pair" "deployer" {
  key_name   = "my-deployer-key"
  public_key = file("~/.ssh/id_ed25519.pub")
}

resource "aws_instance" "social_media_1" {
  ami           = data.aws_ami.ubuntu.id
  instance_type = "t2.micro"
  user_data     = <<-EOF
          #!/bin/bash
          sudo apt update
          sudo apt install -y postgresql
        EOF

  vpc_security_group_ids      = [aws_security_group.social_media.id]
  subnet_id                   = module.vpc.public_subnets[0]
  associate_public_ip_address = true
  key_name                    = aws_key_pair.deployer.key_name

  tags = {
    Name = "social_media_1"
  }
}