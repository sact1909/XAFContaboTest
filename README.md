# XAFContaboTest

## Getting Started
- Install Open SSH
```
Add-WindowsCapability -Online -Name OpenSSH.Server~~0.0.1.0
```
- Open the Port in the Firewall
```
netsh advfirewall firewall add rule name="SSH PORT 22" dir=in action=allow protocol=TCP localport=22
```
- Create public and private SSH Key
```
ssh-keygen -t rsa -b 4096 -C "my-email@test.com"
```
- Override the administrators_authorized_keys to add the public key with administrator permissions
```
type my-public-key.pub >> administrators_authorized_keys
```
- Give permission to the administrators_authorized_keys files
```
icacls administrators_authorized_keys /inheritance:r /grant "Administrators:F" /grant "SYSTEM:F"
```
## Server Source

* [ASP.NET Core Runtime 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) - It will depend on which framework your project use

## Authors

* **Steven Checo** - *Entire work* - [sact1909](https://github.com/sact1909)
