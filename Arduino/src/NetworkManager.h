#pragma once
#include <ESP8266WiFi.h>
#include <ESP8266WebServer.h>
#include <ESP8266HTTPClient.h>
#include <WiFiUdp.h>


class NetworkManager
{
private:
	enum NetworkState
	{
		WAITING_FOR_CREDENTIALS,
		CONNECTED_TO_WIFI,
		CONNECTED_TO_HOST
	};

	NetworkState _curState;
	
	//Broadcast
	static const int UDP_PACKET_SIZE = 17;
	char _udpSendBuffer[UDP_PACKET_SIZE];
	static const uint16_t BROADCAST_PORT = 6678;
	static const uint16_t BROADCAST_DELAY_MS = 5000;

	const String BROADCAST_MESSAGE = "ESP8266_BROADCAST";
	const String HOST_REQUEST = "HOST_REQUEST";

	//TCP pata stuff
	static const uint16_t DATA_PORT = 6676;
	static const int MAX_BYTES_PER_PACKAGE = 256;
	char _DataSendBuffer[MAX_BYTES_PER_PACKAGE];

	bool _ledToggle = false;
	
	uint16_t _updateTimer = 0;
	uint32_t _lastUpdatetime = 0;

	IPAddress _localIP;
	IPAddress _remoteIP;

	//UDP
	WiFiUDP _udp;

	//TCP
	WiFiClient client;

	//Server
	ESP8266WebServer _webServer;

	void InitSendBuffer();
	void CheckUDPResponse();
	void SendUDPBroadcast();
	//If ssid is not specified (empty string) mcu tries to connect to last network
	void TryConnectToNetwork(const String ssid, const String pw);
	///Webserver stuff
	void BeginWebConfig();
	void handleRoot();
	void handleLogin();
	void handleNotFound();
public:	
	NetworkManager();
	~NetworkManager();

	void Begin();
	void Update();

	//
	bool SendData(byte* data, uint length);
};

