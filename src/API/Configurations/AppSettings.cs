﻿namespace API.Configurations;
public class AppSettings
{
    public string Secret { get; set; }
    public int Expiration { get; set; }
    public string Emissor { get; set; }
    public string ValidAt { get; set; }
}