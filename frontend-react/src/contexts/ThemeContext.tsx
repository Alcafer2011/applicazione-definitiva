import React, { createContext, useContext, useState, useEffect, ReactNode } from 'react';

/**
 * AI Enterprise OS - Theme Context (React)
 * Unified theme system for React applications
 */

interface ThemeColors {
  bgVoid: string;
  bgCrystal: string;
  bgGlassHeavy: string;
  bgGlassLight: string;
  neonCyan: string;
  neonBlue: string;
  neonPurple: string;
  neonGreen: string;
  neonPink: string;
  neonOrange: string;
  textPrimary: string;
  textSecondary: string;
  textAccent: string;
  textMuted: string;
  borderCrystal: string;
  borderNeon: string;
  borderLight: string;
  colorSuccess: string;
  colorError: string;
  colorWarning: string;
  colorInfo: string;
}

interface Theme {
  name: string;
  mode: 'dark' | 'light';
  colors: ThemeColors;
}

interface ThemeContextType {
  currentTheme: Theme;
  isDarkMode: boolean;
  setTheme: (themeName: string) => void;
  toggleTheme: () => void;
  getColor: (colorName: keyof ThemeColors) => string;
  themes: Record<string, Theme>;
}

const DARK_THEME: Theme = {
  name: 'Neon Cyber Dark',
  mode: 'dark',
  colors: {
    bgVoid: '#02040a',
    bgCrystal: 'rgba(10, 20, 40, 0.4)',
    bgGlassHeavy: 'rgba(15, 23, 42, 0.7)',
    bgGlassLight: 'rgba(30, 50, 80, 0.3)',
    neonCyan: '#00f2ff',
    neonBlue: '#38bdf8',
    neonPurple: '#bc13fe',
    neonGreen: '#00ff41',
    neonPink: '#ff00ea',
    neonOrange: '#ff8800',
    textPrimary: '#ffffff',
    textSecondary: '#94a3b8',
    textAccent: '#00f2ff',
    textMuted: 'rgba(255, 255, 255, 0.5)',
    borderCrystal: 'rgba(255, 255, 255, 0.15)',
    borderNeon: 'rgba(0, 242, 255, 0.5)',
    borderLight: 'rgba(255, 255, 255, 0.1)',
    colorSuccess: '#00ff41',
    colorError: '#ff00ea',
    colorWarning: '#ff8800',
    colorInfo: '#00f2ff'
  }
};

const LIGHT_THEME: Theme = {
  name: 'Enterprise Light',
  mode: 'light',
  colors: {
    bgVoid: '#ffffff',
    bgCrystal: '#fafafa',
    bgGlassHeavy: '#f5f5f5',
    bgGlassLight: '#f0f0f0',
    neonCyan: '#667eea',
    neonBlue: '#764ba2',
    neonPurple: '#764ba2',
    neonGreen: '#52c41a',
    neonPink: '#f5222d',
    neonOrange: '#faad14',
    textPrimary: 'rgba(0, 0, 0, 0.85)',
    textSecondary: 'rgba(0, 0, 0, 0.45)',
    textAccent: '#667eea',
    textMuted: 'rgba(0, 0, 0, 0.25)',
    borderCrystal: '#d9d9d9',
    borderNeon: '#e6e6e6',
    borderLight: '#f0f0f0',
    colorSuccess: '#52c41a',
    colorError: '#f5222d',
    colorWarning: '#faad14',
    colorInfo: '#1890ff'
  }
};

const ThemeContext = createContext<ThemeContextType | undefined>(undefined);

export const useTheme = () => {
  const context = useContext(ThemeContext);
  if (!context) {
    throw new Error('useTheme must be used within a ThemeProvider');
  }
  return context;
};

interface ThemeProviderProps {
  children: ReactNode;
  defaultTheme?: 'dark' | 'light';
}

export const ThemeProvider: React.FC<ThemeProviderProps> = ({
  children,
  defaultTheme = 'dark'
}) => {
  const [currentTheme, setCurrentTheme] = useState<Theme>(() => {
    // Check localStorage
    const saved = localStorage.getItem('bos-theme');
    if (saved === 'light') return LIGHT_THEME;
    if (saved === 'dark') return DARK_THEME;

    // Check system preference
    const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
    return prefersDark ? DARK_THEME : LIGHT_THEME;
  });

  const themes: Record<string, Theme> = {
    dark: DARK_THEME,
    light: LIGHT_THEME
  };

  useEffect(() => {
    // Apply CSS variables
    const root = document.documentElement;
    Object.entries(currentTheme.colors).forEach(([key, value]) => {
      const cssVarName = `--${key.replace(/([A-Z])/g, '-$1').toLowerCase()}`;
      root.style.setProperty(cssVarName, value);
    });

    // Update HTML attributes
    document.documentElement.setAttribute('data-theme', currentTheme.mode);
    document.body.setAttribute('data-theme', currentTheme.mode);

    // Update theme meta tag
    const metaThemeColor = document.querySelector('meta[name="theme-color"]');
    if (metaThemeColor) {
      metaThemeColor.setAttribute('content', currentTheme.colors.neonCyan);
    }

    // Save to localStorage
    localStorage.setItem('bos-theme', currentTheme.mode);
  }, [currentTheme]);

  const setTheme = (themeName: string) => {
    if (themeName === 'dark') {
      setCurrentTheme(DARK_THEME);
    } else if (themeName === 'light') {
      setCurrentTheme(LIGHT_THEME);
    }
  };

  const toggleTheme = () => {
    setTheme(currentTheme.mode === 'dark' ? 'light' : 'dark');
  };

  const getColor = (colorName: keyof ThemeColors): string => {
    return currentTheme.colors[colorName] || '#ffffff';
  };

  const value: ThemeContextType = {
    currentTheme,
    isDarkMode: currentTheme.mode === 'dark',
    setTheme,
    toggleTheme,
    getColor,
    themes
  };

  return (
    <ThemeContext.Provider value={value}>
      {children}
    </ThemeContext.Provider>
  );
};

export default ThemeContext;
