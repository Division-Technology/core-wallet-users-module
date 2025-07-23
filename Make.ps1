param(
    [Parameter(Mandatory=$true)]
    [ValidateSet("clean","build","prune","up","Analyze","Roslynator","Format")]
    [string]$Target
)

function Clean {
    docker-compose -f docker-compose.dev.yml down --rmi all --volumes --remove-orphans
}

function Build {
    docker-compose -f docker-compose.dev.yml build --no-cache
}

function Prune {
    docker system prune -a -f
}

function Up {
    docker-compose -f docker-compose.dev.yml up
}

$solution = "Users.sln"

function Analyze {
    Roslynator
    Format
}

function Roslynator {
    roslynator analyze $solution
}

function Format {
    dotnet format --verify-no-changes
}

# To run all:
# .\Make.ps1 Analyze

switch ($Target) {
    "clean" { Clean }
    "build" { Build }
    "prune" { Prune }
    "up" { Up }
    "Analyze" { Analyze }
    "Roslynator" { Roslynator }
    "Format" { Format }
} 