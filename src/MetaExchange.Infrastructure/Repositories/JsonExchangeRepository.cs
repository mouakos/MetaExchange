using System.Text.Json;
using CryptoExchange.Application.Interfaces;
using MetaExchange.Domain.Entities;
using MetaExchange.Infrastructure.DTOs;
using MetaExchange.Infrastructure.mappers;

namespace MetaExchange.Infrastructure.Repositories;

public class JsonExchangeRepository : IExchangeRepository
{
    private readonly string m_FolderPath;

    public JsonExchangeRepository(string folderPath)
    {
        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException($"The directory '{folderPath}' does not exist.");

        m_FolderPath = folderPath;
    }

    public async Task<List<Exchange>> GetAllAsync()
    {
        var exchanges = new List<Exchange>();
        var files = Directory.GetFiles(m_FolderPath, "*.json");

        foreach (var file in files)
        {
            var json = await File.ReadAllTextAsync(file);
            var dto = JsonSerializer.Deserialize<ExchangeDto>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (dto != null)
                exchanges.Add(ExchangeMapper.ToDomain(dto));
        }

        return exchanges;
    }
}