@echo off

helm repo add openobserve https://charts.openobserve.ai
helm repo update

helm upgrade --install openobserve --version  0.20.1 openobserve/openobserve-standalone -f "./chart-openobserve/values.yaml" --wait

REM helm show values openobserve/openobserve-standalone > ./chart-openobserve/values.yaml

