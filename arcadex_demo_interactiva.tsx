import React, { useState, useEffect } from 'react';
import { 
  Gamepad2, 
  Search, 
  Library, 
  Store, 
  ShoppingCart, 
  Play, 
  ChevronLeft,
  TerminalSquare,
  Trophy,
  UserCircle
} from 'lucide-react';

// --- MOCK DATA ---
const mockGames = [
  {
    id: 'g1',
    title: 'Neon Samurai: Vengeance',
    developer: 'CyberBlade Studios',
    price: 49.99,
    genre: 'Acción / RPG',
    rating: 4.8,
    image: 'https://images.unsplash.com/photo-1542751371-adc38448a05e?auto=format&fit=crop&q=80&w=800',
    description: 'Ábrete paso a través de una metrópolis ciberpunk dominada por corporaciones corruptas. Mejora tus implantes cibernéticos y domina el arte de la katana de plasma en este RPG de acción trepidante.',
    tags: ['Cyberpunk', 'Espadas', 'Mundo Abierto']
  },
  {
    id: 'g2',
    title: 'Galactic Command 4',
    developer: 'Stellar Forge',
    price: 39.99,
    genre: 'Estrategia (RTS)',
    rating: 4.5,
    image: 'https://images.unsplash.com/photo-1614730321146-b6fa6a46bcb4?auto=format&fit=crop&q=80&w=800',
    description: 'Toma el control de tu propia flota estelar. Construye tu imperio a lo largo de sistemas solares generados proceduralmente y enfréntate a la amenaza de la IA oscura.',
    tags: ['Espacial', 'Táctico', 'Multijugador']
  },
  {
    id: 'g3',
    title: 'Synthwave Skater',
    developer: 'RetroWave Games',
    price: 19.99,
    genre: 'Deportes / Arcade',
    rating: 4.9,
    image: 'https://images.unsplash.com/photo-1551103782-8ab07afd45c1?auto=format&fit=crop&q=80&w=800',
    description: 'Pátina al ritmo de la mejor música synthwave en pistas de neón interminables. Realiza trucos imposibles para multiplicar tu puntuación y escalar en las tablas de clasificación mundiales.',
    tags: ['Música', 'Casual', 'Retro']
  },
  {
    id: 'g4',
    title: 'Void Explorer: Abismo',
    developer: 'Deep Space Indie',
    price: 24.99,
    genre: 'Aventura / Supervivencia',
    rating: 4.2,
    image: 'https://images.unsplash.com/photo-1618336753974-aae8e04506aa?auto=format&fit=crop&q=80&w=800',
    description: 'Tu nave se ha estrellado en un planeta oceánico desconocido. Explora las profundidades abisales, gestiona tu oxígeno y descubre los oscuros secretos que yacen en el fondo.',
    tags: ['Supervivencia', 'Exploración', 'Terror']
  },
  {
    id: 'g5',
    title: 'Pixel Defenders',
    developer: 'Vera & Veloz Co.',
    price: 14.99,
    genre: 'Tower Defense',
    rating: 4.7,
    image: 'https://images.unsplash.com/photo-1550745165-9bc0b252726f?auto=format&fit=crop&q=80&w=800',
    description: 'Defiende el núcleo central de oleadas de virus informáticos. Un clásico tower defense con gráficos pixel art hiper-estilizados y mecánicas de programación integradas.',
    tags: ['Estrategia', 'Pixel Art', 'Retro']
  },
  {
    id: 'g6',
    title: 'Apex Racer 2077',
    developer: 'Velocity Engine',
    price: 59.99,
    genre: 'Carreras',
    rating: 4.6,
    image: 'https://images.unsplash.com/photo-1547394765-185e1e68f34e?auto=format&fit=crop&q=80&w=800',
    description: 'La experiencia de conducción del futuro. Vehículos anti-gravedad, pistas magnéticas y velocidades que superan la barrera del sonido en la liga Apex.',
    tags: ['Simulación', 'Carreras', 'VR Soportado']
  }
];

export default function ArcadeXDemo() {
  const [currentView, setCurrentView] = useState('store'); // 'store', 'library', 'game'
  const [selectedGame, setSelectedGame] = useState(null);
  const [library, setLibrary] = useState(['g5']); // Start with one game owned
  const [searchQuery, setSearchQuery] = useState('');
  const [isPurchasing, setIsPurchasing] = useState(false);
  const [toastMessage, setToastMessage] = useState(null);

  // Funciones de navegación
  const goToStore = () => { setCurrentView('store'); setSelectedGame(null); window.scrollTo(0,0); };
  const goToLibrary = () => { setCurrentView('library'); setSelectedGame(null); window.scrollTo(0,0); };
  const viewGameDetail = (game) => { setSelectedGame(game); setCurrentView('game'); window.scrollTo(0,0); };

  // Sistema de notificaciones (Toast)
  const showToast = (message) => {
    setToastMessage(message);
    setTimeout(() => setToastMessage(null), 3000);
  };

  // Simulación de compra
  const handlePurchase = (gameId) => {
    setIsPurchasing(true);
    setTimeout(() => {
      setLibrary([...library, gameId]);
      setIsPurchasing(false);
      showToast('¡Juego añadido a tu biblioteca!');
      goToLibrary();
    }, 1500);
  };

  // Simulación de jugar
  const handlePlay = (gameTitle) => {
    showToast(`Iniciando ${gameTitle}... ¡Que lo disfrutes!`);
  };

  // Filtrar juegos
  const filteredGames = mockGames.filter(game => 
    game.title.toLowerCase().includes(searchQuery.toLowerCase()) || 
    game.genre.toLowerCase().includes(searchQuery.toLowerCase())
  );

  const ownedGames = mockGames.filter(game => library.includes(game.id));

  // --- COMPONENTES UI ---

  const Navbar = () => (
    <nav className="sticky top-0 z-50 bg-[#0B0A1A]/90 backdrop-blur-md border-b border-[#00F0FF]/30 px-6 py-4 flex justify-between items-center shadow-[0_0_20px_rgba(0,240,255,0.1)]">
      <div className="flex items-center gap-3 cursor-pointer" onClick={goToStore}>
        <Gamepad2 className="text-[#FF007F] w-8 h-8 drop-shadow-[0_0_8px_rgba(255,0,127,0.8)]" />
        <h1 className="text-3xl font-black text-white tracking-wider font-['Montserrat'] drop-shadow-[0_0_10px_rgba(0,240,255,0.5)]">
          ARCADE<span className="text-[#00F0FF]">X</span>
        </h1>
      </div>
      
      <div className="flex items-center gap-6">
        <button 
          onClick={goToStore} 
          className={`flex items-center gap-2 font-bold uppercase tracking-wide transition-all ${currentView === 'store' ? 'text-[#00F0FF] border-b-2 border-[#00F0FF]' : 'text-[#D1CFE2] hover:text-white'}`}
        >
          <Store className="w-5 h-5" /> Tienda
        </button>
        <button 
          onClick={goToLibrary} 
          className={`flex items-center gap-2 font-bold uppercase tracking-wide transition-all ${currentView === 'library' ? 'text-[#FF007F] border-b-2 border-[#FF007F]' : 'text-[#D1CFE2] hover:text-white'}`}
        >
          <Library className="w-5 h-5" /> Biblioteca
          {library.length > 0 && (
            <span className="bg-[#FF007F] text-white text-xs rounded-full w-5 h-5 flex items-center justify-center">{library.length}</span>
          )}
        </button>
      </div>

      <div className="flex items-center gap-4">
        <div className="text-right hidden md:block">
          <p className="text-[#00F0FF] text-sm font-bold leading-tight">UsuarioGamer_99</p>
          <p className="text-[#A09EBA] text-xs">Nivel 42</p>
        </div>
        <UserCircle className="w-10 h-10 text-[#D1CFE2]" />
      </div>
    </nav>
  );

  const Toast = () => {
    if (!toastMessage) return null;
    return (
      <div className="fixed bottom-6 right-6 z-50 bg-[#16122B] border-l-4 border-[#00F0FF] text-white px-6 py-4 rounded shadow-[0_0_20px_rgba(0,240,255,0.3)] flex items-center gap-3 animate-bounce">
        <Trophy className="text-[#00F0FF] w-6 h-6" />
        <p className="font-bold">{toastMessage}</p>
      </div>
    );
  };

  const Footer = () => (
    <footer className="mt-20 border-t border-[#FF007F]/30 bg-[#0B0A1A] py-8 text-center">
      <p className="text-[#A09EBA] mb-2 font-['Montserrat'] uppercase tracking-widest text-sm">ArcadeX Platform Concept</p>
      <div className="flex justify-center items-center gap-4 text-sm text-[#D1CFE2]">
        <span className="flex items-center gap-1"><TerminalSquare className="w-4 h-4 text-[#00F0FF]" /> Desarrollado por:</span>
        <strong className="text-[#00F0FF]">Cristopher Vera</strong>
        <span className="text-[#FF007F]">&amp;</span>
        <strong className="text-[#00F0FF]">Jefferson Veloz</strong>
      </div>
    </footer>
  );

  // --- VISTAS ---

  const StoreView = () => (
    <div className="p-8 max-w-7xl mx-auto animate-fadeIn">
      <div className="mb-10 flex flex-col md:flex-row justify-between items-center gap-6">
        <div>
          <h2 className="text-5xl font-black text-white font-['Montserrat'] uppercase mb-2">
            Catálogo <span className="text-[#FF007F]">Destacado</span>
          </h2>
          <p className="text-[#A09EBA] text-lg">Descubre los mejores títulos seleccionados para ti.</p>
        </div>
        
        <div className="relative w-full md:w-96">
          <input 
            type="text" 
            placeholder="Buscar juegos, géneros..." 
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="w-full bg-[#16122B] border border-[#00F0FF]/50 text-white rounded-full py-3 px-5 pl-12 focus:outline-none focus:border-[#00F0FF] focus:shadow-[0_0_15px_rgba(0,240,255,0.3)] transition-all"
          />
          <Search className="absolute left-4 top-3.5 text-[#00F0FF] w-5 h-5" />
        </div>
      </div>

      {filteredGames.length === 0 ? (
        <div className="text-center py-20 text-[#A09EBA]">
          <Search className="w-16 h-16 mx-auto mb-4 opacity-50" />
          <p className="text-xl">No se encontraron juegos para "{searchQuery}"</p>
        </div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {filteredGames.map(game => (
            <div 
              key={game.id} 
              onClick={() => viewGameDetail(game)}
              className="bg-[#16122B] rounded-xl overflow-hidden cursor-pointer border border-transparent hover:border-[#FF007F] transition-all duration-300 hover:shadow-[0_0_25px_rgba(255,0,127,0.2)] hover:-translate-y-2 group"
            >
              <div className="h-48 overflow-hidden relative">
                <img src={game.image} alt={game.title} className="w-full h-full object-cover transition-transform duration-700 group-hover:scale-110" />
                <div className="absolute inset-0 bg-gradient-to-t from-[#16122B] to-transparent opacity-80"></div>
                {library.includes(game.id) && (
                  <div className="absolute top-3 right-3 bg-[#00F0FF] text-[#0B0A1A] text-xs font-bold px-2 py-1 rounded">EN BIBLIOTECA</div>
                )}
              </div>
              <div className="p-5">
                <p className="text-[#00F0FF] text-xs font-bold uppercase tracking-wider mb-1">{game.genre}</p>
                <h3 className="text-xl font-bold text-white mb-2 font-['Montserrat'] leading-tight">{game.title}</h3>
                <div className="flex justify-between items-center mt-4">
                  <span className="text-[#A09EBA] text-sm flex items-center gap-1">
                    <span className="text-yellow-400">★</span> {game.rating}
                  </span>
                  <span className="bg-[#0B0A1A] text-white font-bold py-1 px-3 rounded border border-[#00F0FF]/30 group-hover:border-[#00F0FF] transition-colors">
                    ${game.price}
                  </span>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );

  const GameDetailView = () => {
    if (!selectedGame) return null;
    const isOwned = library.includes(selectedGame.id);

    return (
      <div className="animate-fadeIn">
        <div className="relative h-[60vh] w-full overflow-hidden">
          <img src={selectedGame.image} alt={selectedGame.title} className="w-full h-full object-cover filter brightness-50" />
          <div className="absolute inset-0 bg-gradient-to-t from-[#05050A] via-[#05050A]/60 to-transparent"></div>
          
          <div className="absolute bottom-0 left-0 w-full p-8 max-w-7xl mx-auto left-0 right-0">
            <button onClick={goToStore} className="flex items-center gap-2 text-[#00F0FF] hover:text-white mb-6 transition-colors">
              <ChevronLeft className="w-5 h-5" /> Volver a la Tienda
            </button>
            <div className="flex flex-col md:flex-row gap-8 items-end">
              <img src={selectedGame.image} alt="Cover" className="w-48 h-64 object-cover rounded-xl border-2 border-[#FF007F] shadow-[0_0_30px_rgba(255,0,127,0.4)] hidden md:block" />
              <div className="flex-grow">
                <p className="text-[#00F0FF] font-bold uppercase tracking-widest mb-2">{selectedGame.developer}</p>
                <h1 className="text-6xl font-black text-white font-['Montserrat'] uppercase mb-4 text-shadow-sm leading-none">{selectedGame.title}</h1>
                <div className="flex flex-wrap gap-2 mb-6">
                  {selectedGame.tags.map(tag => (
                    <span key={tag} className="bg-[#16122B] border border-[#00F0FF]/30 text-[#D1CFE2] px-3 py-1 rounded-full text-sm">
                      {tag}
                    </span>
                  ))}
                </div>
              </div>
            </div>
          </div>
        </div>

        <div className="max-w-7xl mx-auto p-8 grid grid-cols-1 md:grid-cols-3 gap-12">
          <div className="md:col-span-2">
            <h3 className="text-2xl font-bold text-[#FF007F] font-['Montserrat'] uppercase mb-4">Acerca del Juego</h3>
            <p className="text-[#D1CFE2] text-lg leading-relaxed mb-8">{selectedGame.description}</p>
            
            <div className="bg-[#16122B] p-6 rounded-xl border border-white/10">
              <h4 className="text-white font-bold mb-4">Requisitos del Sistema (Recomendados)</h4>
              <ul className="text-[#A09EBA] space-y-2 text-sm grid grid-cols-2">
                <li><strong>SO:</strong> Windows 11 64-bit</li>
                <li><strong>Proc:</strong> Intel Core i7 / AMD Ryzen 7</li>
                <li><strong>Memoria:</strong> 16 GB RAM</li>
                <li><strong>Gráficos:</strong> RTX 3060 / RX 6700 XT</li>
                <li><strong>DirectX:</strong> Versión 12</li>
                <li><strong>Almacenamiento:</strong> 50 GB SSD</li>
              </ul>
            </div>
          </div>

          <div className="bg-[#16122B] p-8 rounded-xl border-t-4 border-[#00F0FF] h-fit sticky top-24 shadow-2xl">
            {isOwned ? (
              <div className="text-center">
                <div className="bg-[#0B0A1A] text-[#00F0FF] py-3 rounded-lg mb-6 font-bold border border-[#00F0FF]/30 flex items-center justify-center gap-2">
                  <Library className="w-5 h-5" /> Ya tienes este juego
                </div>
                <button 
                  onClick={() => handlePlay(selectedGame.title)}
                  className="w-full bg-gradient-to-r from-[#FF007F] to-[#00F0FF] text-white font-black uppercase tracking-widest py-4 rounded-lg hover:shadow-[0_0_20px_rgba(0,240,255,0.5)] transition-all flex items-center justify-center gap-3 text-lg"
                >
                  <Play className="w-6 h-6 fill-current" /> Jugar Ahora
                </button>
              </div>
            ) : (
              <div>
                <div className="text-5xl font-black text-white mb-6 text-center">${selectedGame.price}</div>
                <button 
                  onClick={() => handlePurchase(selectedGame.id)}
                  disabled={isPurchasing}
                  className={`w-full font-black uppercase tracking-widest py-4 rounded-lg transition-all flex items-center justify-center gap-3 text-lg
                    ${isPurchasing 
                      ? 'bg-gray-600 cursor-not-allowed' 
                      : 'bg-[#FF007F] hover:bg-white hover:text-[#FF007F] text-white hover:shadow-[0_0_20px_rgba(255,0,127,0.6)]'
                    }`}
                >
                  {isPurchasing ? (
                    <span className="animate-pulse">Procesando pago...</span>
                  ) : (
                    <><ShoppingCart className="w-6 h-6" /> Comprar Juego</>
                  )}
                </button>
                <p className="text-center text-[#A09EBA] text-sm mt-4">Transacción segura cifrada mediante ArcadeX Pay.</p>
              </div>
            )}
          </div>
        </div>
      </div>
    );
  };

  const LibraryView = () => (
    <div className="p-8 max-w-7xl mx-auto animate-fadeIn min-h-[60vh]">
      <h2 className="text-5xl font-black text-white font-['Montserrat'] uppercase mb-2 border-l-8 border-[#00F0FF] pl-4">
        Mi <span className="text-[#00F0FF]">Biblioteca</span>
      </h2>
      <p className="text-[#A09EBA] text-lg mb-10 pl-6">Tienes {library.length} juegos en tu cuenta.</p>

      {ownedGames.length === 0 ? (
        <div className="text-center py-20 bg-[#16122B] rounded-xl border border-dashed border-[#FF007F]/50">
          <Gamepad2 className="w-20 h-20 mx-auto mb-4 text-[#A09EBA] opacity-30" />
          <p className="text-2xl text-white font-bold mb-4">Tu biblioteca está vacía</p>
          <button onClick={goToStore} className="bg-[#00F0FF] text-[#0B0A1A] px-6 py-2 rounded font-bold hover:bg-white transition-colors">
            Explorar la Tienda
          </button>
        </div>
      ) : (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
          {ownedGames.map(game => (
            <div key={game.id} className="bg-[#16122B] rounded-xl overflow-hidden border border-white/5 relative group">
              <div className="h-64 overflow-hidden">
                <img src={game.image} alt={game.title} className="w-full h-full object-cover filter brightness-75 group-hover:brightness-100 transition-all duration-300" />
              </div>
              
              {/* Overlay de Jugar */}
              <div className="absolute inset-0 bg-[#0B0A1A]/80 flex flex-col items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity duration-300 backdrop-blur-sm">
                <button 
                  onClick={() => handlePlay(game.title)}
                  className="bg-[#00F0FF] text-[#0B0A1A] w-16 h-16 rounded-full flex items-center justify-center mb-4 hover:scale-110 hover:shadow-[0_0_20px_rgba(0,240,255,0.6)] transition-all"
                >
                  <Play className="w-8 h-8 ml-1 fill-current" />
                </button>
                <p className="text-white font-bold text-center px-4">{game.title}</p>
                <button onClick={() => viewGameDetail(game)} className="mt-4 text-[#FF007F] text-sm hover:text-white underline">
                  Ver Detalles
                </button>
              </div>
              
              {/* Info inferior (visible si no hay hover en mobile) */}
              <div className="absolute bottom-0 w-full bg-gradient-to-t from-[#0B0A1A] to-transparent p-4 pt-10 group-hover:opacity-0 transition-opacity">
                <h3 className="text-white font-bold truncate text-lg">{game.title}</h3>
                <p className="text-[#00F0FF] text-xs">Instalado - Listo para jugar</p>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );

  return (
    <div className="min-h-screen bg-[#05050A] text-[#D1CFE2] font-['Nunito'] selection:bg-[#FF007F] selection:text-white">
      <Navbar />
      
      <main className="pb-10">
        {currentView === 'store' && <StoreView />}
        {currentView === 'game' && <GameDetailView />}
        {currentView === 'library' && <LibraryView />}
      </main>

      <Footer />
      <Toast />

      {/* Global Styles para animaciones simples */}
      <style dangerouslySetInnerHTML={{__html: `
        @keyframes fadeIn {
          from { opacity: 0; transform: translateY(10px); }
          to { opacity: 1; transform: translateY(0); }
        }
        .animate-fadeIn {
          animation: fadeIn 0.4s ease-out forwards;
        }
      `}} />
    </div>
  );
}