FROM node:18-alpine AS build

# Add labels
LABEL maintainer="cucdenis@gmail.com"
LABEL description="Task Manager Frontend Application"

WORKDIR /app

# Copy package files and install dependencies
COPY package*.json ./
RUN npm install

# Copy source code
COPY . .

# Set environment variables for build
ENV VITE_API_URL=http://localhost:5000/api
ENV VITE_API_TIMEOUT=30000

# Build the app
RUN npm run build

# Production stage
FROM nginx:alpine AS production

# Copy built assets from build stage
COPY --from=build /app/dist /usr/share/nginx/html

# Copy nginx config
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
