﻿using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Responses;
using System;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
    public class DwollaHttpService
    {
        private IDwollaClient _client;
        public IDwollaClient Client
        {
            get => _client ?? DwollaConfiguration.Client;
            set => _client = value;
        }

        private static TokenResponse _cachedToken;
        private static DateTime _expiresAtUtc;

        private readonly Func<Task<string>> _getAccessTokenAsync;

        private async Task<string> GetAccessTokenAsync()
        {
            if (_cachedToken != null && _expiresAtUtc > DateTime.UtcNow)
            {
                return _cachedToken.Token;
            }

            var response = await Authorization.GetToken();

            if (response.Error != null)
            {
                throw new Exception(response.Error.Message);
            }

            _cachedToken = response.Content;
            _expiresAtUtc = DateTime.UtcNow.AddSeconds(response.Content.ExpiresIn);

            return _cachedToken.Token;
        }

        public DwollaHttpService()
            : this(null, null) { }

        public DwollaHttpService(Func<Task<string>> getAccessTokenAsync)
            : this(null, getAccessTokenAsync) { }

        public DwollaHttpService(IDwollaClient dwollaClient)
            : this(dwollaClient, null) { }

        public DwollaHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessTokenAsync)
        {
            _getAccessTokenAsync = getAccessTokenAsync;

            if (_getAccessTokenAsync == null)
            {
                _getAccessTokenAsync = GetAccessTokenAsync;
            }

            _client = dwollaClient;
        }

        public async Task AccessTokenAsync()
        {
            await _getAccessTokenAsync();
        }

        private AuthorizationHttpService _authorization;
        public AuthorizationHttpService Authorization
        {
            get
            {
                return _authorization ?? (_authorization = new AuthorizationHttpService(Client, _getAccessTokenAsync));
            }
        }

        private FundingSourcesHttpService _fundingSources;
        public FundingSourcesHttpService FundingSources
        {
            get
            {
                return _fundingSources ?? (_fundingSources = new FundingSourcesHttpService(Client, _getAccessTokenAsync));
            }
        }

        public BeneficialOwnersHttpService _beneficialOwners;
        public BeneficialOwnersHttpService BeneficialOwners
        {
            get
            {
                return _beneficialOwners ?? (_beneficialOwners = new BeneficialOwnersHttpService(Client, _getAccessTokenAsync));
            }
        }

        public BusinessClassificationHttpService _businessClassification;
        public BusinessClassificationHttpService BusinessClassification
        {
            get
            {
                return _businessClassification ?? (_businessClassification = new BusinessClassificationHttpService(Client, _getAccessTokenAsync));
            }
        }

        public DocumentsHttpService _documents;
        public DocumentsHttpService Documents
        {
            get
            {
                return _documents ?? (_documents = new DocumentsHttpService(Client, _getAccessTokenAsync));
            }
        }

        public EventsHttpService _events;
        public EventsHttpService Events
        {
            get
            {
                return _events ?? (_events = new EventsHttpService(Client, _getAccessTokenAsync));
            }
        }

        public ExchangesHttpService _exchanges;
        public ExchangesHttpService Exchanges
        {
            get
            {
                return _exchanges ?? (_exchanges = new ExchangesHttpService(Client, _getAccessTokenAsync));
            }
        }

        public LabelsHttpService _labels;
        public LabelsHttpService Labels
        {
            get
            {
                return _labels ?? (_labels = new LabelsHttpService(Client, _getAccessTokenAsync));
            }
        }

        public MassPaymentsHttpService _masspayments;
        public MassPaymentsHttpService MassPayments
        {
            get
            {
                return _masspayments ?? (_masspayments = new MassPaymentsHttpService(Client, _getAccessTokenAsync));
            }
        }

        public TransfersHttpService _transfers;
        public TransfersHttpService Transfers
        {
            get
            {
                return _transfers ?? (_transfers = new TransfersHttpService(Client, _getAccessTokenAsync));
            }
        }

        public CustomersHttpService _customers;
        public CustomersHttpService Customers
        {
            get
            {
                return _customers ?? (_customers = new CustomersHttpService(Client, _getAccessTokenAsync));
            }
        }

        public WebhookSubscriptionsHttpService _webhookSubscriptions;
        public WebhookSubscriptionsHttpService WebhookSubscriptions
        {
            get
            {
                return _webhookSubscriptions ?? (_webhookSubscriptions = new WebhookSubscriptionsHttpService(Client, _getAccessTokenAsync));
            }
        }

        public RootHttpService _root;
        public RootHttpService Root
        {
            get
            {
                return _root ?? new RootHttpService(Client, _getAccessTokenAsync);
            }
        }
    }
}