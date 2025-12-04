# DevOps CI/CD Demo Project (.NET 8)

A demonstration project showcasing CI/CD pipeline implementation using GitHub Actions, Docker, and ASP.NET Core.

## 🚀 Features

- **ASP.NET Core 8 Minimal API**: Modern, high-performance web framework
- **Automated Testing**: XUnit tests with WebApplicationFactory
- **Docker Containerization**: Multi-stage builds for optimized images
- **CI/CD Pipeline**: Automated build, test, and deploy workflow
- **Swagger/OpenAPI**: Built-in API documentation
- **Health Checks**: Production-ready health monitoring endpoints

## 📋 Prerequisites

- .NET 8 SDK
- Docker & Docker Compose
- Git
- GitHub account
- Docker Hub account (for image registry)

## 🛠️ Project Structure

```
.
├── Program.cs                      # ASP.NET Core application
├── DevOpsDemo.csproj              # Project file
├── appsettings.json               # Configuration
├── appsettings.Development.json   # Dev configuration
├── Dockerfile                      # Multi-stage Docker build
├── docker-compose.yml              # Local development setup
├── tests/
│   ├── ProgramTests.cs            # XUnit tests
│   └── DevOpsDemo.Tests.csproj    # Test project file
├── .github/
│   └── workflows/
│       └── ci-cd.yml              # GitHub Actions pipeline
└── README.md
```

## 🏃 Running Locally

### Option 1: Using .NET CLI
```bash
# Restore dependencies
dotnet restore

# Run the application
dotnet run

# The app will be available at http://localhost:5000
# Swagger UI at http://localhost:5000/swagger

# Run tests
dotnet test tests/DevOpsDemo.Tests.csproj
```

### Option 2: Using Docker
```bash
# Build the image
docker build -t devops-demo-dotnet .

# Run the container
docker run -p 8080:8080 devops-demo-dotnet

# Access at http://localhost:8080
```

### Option 3: Using Docker Compose
```bash
# Start the application
docker-compose up

# Run in detached mode
docker-compose up -d

# View logs
docker-compose logs -f

# Stop the application
docker-compose down
```

## 🌐 API Endpoints

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/` | GET | Welcome message |
| `/health` | GET | Health check endpoint |
| `/api/info` | GET | Application information |
| `/swagger` | GET | OpenAPI documentation (dev only) |

### Example Requests

```bash
# Get home
curl http://localhost:8080/

# Health check
curl http://localhost:8080/health

# Get app info
curl http://localhost:8080/api/info
```

## 🔄 CI/CD Pipeline

The pipeline automatically:

### 1. **Test Stage**: Runs on every push/PR
   - Checks out code
   - Sets up .NET 8 SDK
   - Restores dependencies
   - Builds the application
   - Runs unit tests with XUnit
   - Uploads test results as artifacts

### 2. **Code Analysis Stage**: Ensures code quality
   - Runs dotnet format to check code formatting
   - Can be extended with additional analyzers

### 3. **Build Stage**: Runs on push to main branch
   - Builds multi-stage Docker image
   - Tags with commit SHA and 'latest'
   - Pushes to Docker Hub
   - Uses BuildKit caching for faster builds

### 4. **Deploy Stage**: Runs after successful build
   - Placeholder for deployment logic
   - Includes health check simulation
   - Can be extended to deploy to Azure, AWS, or Kubernetes

## ⚙️ Setup Instructions

### 1. Create the project structure

```bash
# Create main project
mkdir DevOpsDemo
cd DevOpsDemo
dotnet new web -n DevOpsDemo

# Create test project
mkdir tests
cd tests
dotnet new xunit -n DevOpsDemo.Tests
dotnet add reference ../DevOpsDemo.csproj
cd ..

# Add necessary packages
dotnet add package Microsoft.AspNetCore.OpenApi
dotnet add package Swashbuckle.AspNetCore

# Add test packages
cd tests
dotnet add package Microsoft.AspNetCore.Mvc.Testing
cd ..
```

### 2. Add the files from artifacts above

Replace `Program.cs` and create all other files as shown in the artifacts.

### 3. Make Program class public for testing

Add this line at the end of `Program.cs`:
```csharp
public partial class Program { }
```

### 4. Set up GitHub Secrets

Go to your repository settings → Secrets and variables → Actions, and add:

- `DOCKER_USERNAME`: Your Docker Hub username
- `DOCKER_PASSWORD`: Your Docker Hub password or access token

### 5. Push to trigger pipeline

```bash
git init
git add .
git commit -m "Initial commit: .NET DevOps demo"
git branch -M main
git remote add origin https://github.com/yourusername/devops-demo-dotnet.git
git push -u origin main
```

The GitHub Actions workflow will automatically trigger!

## 📊 Monitoring the Pipeline

- Navigate to the "Actions" tab in your GitHub repository
- Click on the latest workflow run
- View logs for each job (test, code-analysis, build-docker, deploy)
- Download test results artifacts if tests fail

## 🎯 Interview Talking Points

### **Technical Decisions:**
- **Why .NET 8?** Modern, high-performance, cross-platform, excellent for microservices
- **Why Minimal APIs?** Less boilerplate, faster startup, perfect for small services
- **Why Multi-stage Docker builds?** Smaller final image (SDK not included), better security
- **Why XUnit?** Industry standard for .NET testing, excellent tooling support
- **Quality gates**: Tests must pass, code formatting checked before building image

### **DevOps Principles Demonstrated:**
- **Automation**: Everything runs automatically on git push
- **Continuous Integration**: Code tested and analyzed on every commit
- **Continuous Deployment**: Successful builds pushed to registry automatically
- **Infrastructure as Code**: Pipeline and infrastructure defined in version control
- **Container Best Practices**: Multi-stage builds, non-root user, minimal base images
- **Observability**: Health checks, structured logging ready

### **.NET-Specific Best Practices:**
- Uses minimal APIs for simplicity and performance
- Implements proper dependency injection patterns
- Follows .NET configuration conventions
- Uses InvariantGlobalization for smaller Docker images
- Implements proper test isolation with WebApplicationFactory

### **Potential Improvements:**
- Add integration tests with TestContainers
- Implement EF Core with database migrations
- Add authentication/authorization (JWT, OAuth)
- Set up Azure Application Insights or similar monitoring
- Deploy to Azure App Service or AKS (Azure Kubernetes Service)
- Add Redis caching layer
- Implement rate limiting and API versioning
- Add Serilog for structured logging
- Implement distributed tracing with OpenTelemetry
- Add security scanning with Snyk or Trivy

## 🔧 Extending the Project

Ideas to discuss in interviews:
- "I could add Bicep/Terraform to provision Azure infrastructure"
- "Would implement Helm charts for Kubernetes deployment"
- "Could add Dapr for microservices patterns"
- "Would integrate Entity Framework Core with SQL Server"
- "Could add MediatR for CQRS pattern implementation"
- "Would implement Azure Service Bus for message queuing"
- "Could add Polly for resilience policies (retry, circuit breaker)"

## 🐳 Docker Build Optimization

The Dockerfile uses multi-stage builds:
- **Build stage**: Uses full SDK to compile
- **Publish stage**: Creates optimized release build
- **Runtime stage**: Uses minimal aspnet runtime (much smaller)

Result: Final image is ~220MB vs ~700MB with SDK included!

## 📝 License

MIT License - feel free to use for learning and interviews!
