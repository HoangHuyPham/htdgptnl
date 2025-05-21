import React, { MouseEvent, useContext, useState } from 'react';
import { toast } from 'react-toastify';
import { AppRequest }  from '@requests';
import { USER_ACTION, UserContext } from '@contexts/UserContext';
import { IUser } from '@interfaces';

export const LoginPage: React.FC = () => {
  const [username, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState<string | null>(null);
  const {user, dispatch} = useContext(UserContext)

  const handleLogin = async (e: MouseEvent) => {
      let resp = await AppRequest.getInstance().post("api/Auth/login", {username, password})

      if (resp.data?.message === "login success"){
        localStorage.setItem("jwt", resp.data?.data)
        
      }

      // fetch user info
      resp = await AppRequest.getInstance().get("api/UserInfo/me")
      if (resp.data?.message === "Success"){
        dispatch({
          type: USER_ACTION.ADD,
          payload: {
              ...resp.data?.data
          } as IUser
        })
      }
  };

  return (
    <div className="min-h-screen bg-[#f5f5f5] flex items-center justify-center px-4">
      <div className="max-w-md w-full rounded-xl shadow-lg p-8 space-y-6">
        <h1 className="text-3xl font-bold text-center text-gray-900">
          Login
        </h1>
        {error && (
          <div className="bg-red-100 text-red-700 p-3 rounded-md text-center">
            {error}
          </div>
        )}
        <div className="space-y-6">
          <div>
            <label
              htmlFor="username"
              className="block text-sm font-medium text-gray-700 mb-1"
            >
              Username
            </label>
            <input
              id="username"
              type="username"
              value={username}
              onChange={(e) => setEmail(e.target.value)}
              required
              className="w-full px-4 py-3 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-purple-500 focus:border-transparent transition"
              placeholder="username / email"
              autoComplete="username"
            />
          </div>
          <div>
            <label
              htmlFor="password"
              className="block text-sm font-medium text-gray-700 mb-1"
            >
              Password
            </label>
            <input
              id="password"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              className="w-full px-4 py-3 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-purple-500 focus:border-transparent transition"
              placeholder="Your password"
              autoComplete="current-password"
            />
          </div>
          <button
            onClick={handleLogin}
            className="w-full cursor-pointer bg-purple-600 hover:bg-purple-700 text-white font-semibold py-3 rounded-md shadow-md transition focus:outline-none focus:ring-2 focus:ring-purple-500 focus:ring-offset-2"
          >
            Log In
          </button>
        </div>
      </div>
    </div>
  );
};

