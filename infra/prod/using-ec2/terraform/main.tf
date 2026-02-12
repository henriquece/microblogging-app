provider "aws" {
  region = var.region
}

data "aws_ami" "ubuntu" {
  most_recent = true

  filter {
    name   = "name"
    values = ["ubuntu/images/hvm-ssd-gp3/ubuntu-noble-24.04-arm64-server-*"]
  }

  owners = ["099720109477"] # Canonical
}

resource "aws_key_pair" "deployer" {
  key_name   = "my-deployer-key"
  public_key = file("~/.ssh/id_ed25519.pub")
}

resource "aws_instance" "microblogging_app_1" {
  ami           = data.aws_ami.ubuntu.id
  instance_type = "t4g.micro"
  user_data     = <<-EOF
        #!/bin/bash
        curl -fsSL https://get.docker.com -o get-docker.sh
        
        sudo sh ./get-docker.sh

        git clone https://github.com/henriquece/microblogging-app.git
      EOF

  vpc_security_group_ids      = [aws_security_group.microblogging_app.id]
  subnet_id                   = module.vpc.public_subnets[0]
  associate_public_ip_address = true
  key_name                    = aws_key_pair.deployer.key_name

  tags = {
    Name = "microblogging_app_1"
  }
}