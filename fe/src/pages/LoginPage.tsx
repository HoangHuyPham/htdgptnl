import React from 'react';
import { LoginForm } from '@components/LoginForm';

export const LoginPage: React.FC = () => {
  return (
    <div className="min-h-screen bg-[#f5f5f5] flex items-center justify-center px-4">
        <LoginForm />
    </div>
  );
};

