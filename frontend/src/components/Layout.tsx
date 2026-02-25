import { ReactNode, useEffect, useState } from 'react';
import Navbar from './Navbar';
import ChatWidget from './ChatWidget';
import { api } from '../api/client';

export default function Layout({ children }: { children: ReactNode }) {
  const [chatEnabled, setChatEnabled] = useState(false);

  useEffect(() => {
    api.getFeatures().then(f => setChatEnabled(f.chatEnabled)).catch(() => {});
  }, []);

  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      <main className="max-w-7xl mx-auto px-4 py-8">{children}</main>
      {chatEnabled && <ChatWidget />}
    </div>
  );
}
