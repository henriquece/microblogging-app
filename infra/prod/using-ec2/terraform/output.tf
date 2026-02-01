output "instance_public_dns" {
  description = "Public DNS name of the EC2 instance."
  value       = aws_instance.microblogging_app.public_dns
}