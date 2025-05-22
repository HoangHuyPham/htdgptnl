import React, { MouseEvent, useContext, useEffect, useState } from 'react';
import { AppRequest }  from '@requests';
import { USER_ACTION, UserContext } from '@contexts/UserContext';
import { IUser } from '@interfaces';
import { Form } from '@components';
import { LoginForm } from '@components/LoginForm';
import { validToken } from "@utils"
import { useNavigate } from 'react-router';

export const LoginPage: React.FC = () => {
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate()

  useEffect(()=>{
    handleStart()
  }, [])

  const handleStart = async ()=>{
    const token = localStorage.getItem("jwt")

    if (token && validToken(token)){
      navigate("/home")
      return
    }
  }
  
  return (
    
    <div className="min-h-screen bg-[#f5f5f5] flex items-center justify-center px-4">
        <LoginForm />
    </div>
  );
};

