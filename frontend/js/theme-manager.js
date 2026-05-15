/**
 * AI Enterprise OS - Theme Manager
 * Unified theme system for all frontends
 */

class ThemeManager {
  constructor() {
    this.themes = {
      dark: {
        name: 'Neon Cyber Dark',
        colors: {
          bgVoid: '#02040a',
          bgCrystal: 'rgba(10, 20, 40, 0.4)',
          bgGlassHeavy: 'rgba(15, 23, 42, 0.7)',
          neonCyan: '#00f2ff',
          neonBlue: '#38bdf8',
          neonPurple: '#bc13fe',
          neonGreen: '#00ff41',
          neonPink: '#ff00ea',
          textPrimary: '#ffffff',
          textSecondary: '#94a3b8',
          textAccent: '#00f2ff',
          borderCrystal: 'rgba(255, 255, 255, 0.15)',
          borderNeon: 'rgba(0, 242, 255, 0.5)'
        },
        css: 'frontend/css/theme.css'
      },
      light: {
        name: 'Enterprise Light',
        colors: {
          bgPrimary: '#ffffff',
          bgSecondary: '#fafafa',
          bgTertiary: '#f5f5f5',
          textPrimary: 'rgba(0, 0, 0, 0.85)',
          textSecondary: 'rgba(0, 0, 0, 0.45)',
          borderPrimary: '#d9d9d9',
          accentPrimary: '#667eea',
          accentSecondary: '#764ba2'
        },
        css: 'frontend/css/theme-light.css'
      }
    };

    this.currentTheme = this.getSavedTheme() || 'dark';
    this.themeElements = new Map();
    this.init();
  }

  init() {
    this.applyTheme(this.currentTheme);
    this.setupSystemPreference();
    this.setupLocalStorage();
  }

  getSavedTheme() {
    return localStorage.getItem('bos-theme') || null;
  }

  saveTheme(themeName) {
    localStorage.setItem('bos-theme', themeName);
  }

  getSystemPreference() {
    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
  }

  setupSystemPreference() {
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', (e) => {
      if (!localStorage.getItem('bos-theme-manual')) {
        this.setTheme(e.matches ? 'dark' : 'light');
      }
    });
  }

  setupLocalStorage() {
    // Listen for changes in other tabs
    window.addEventListener('storage', (e) => {
      if (e.key === 'bos-theme') {
        this.setTheme(e.newValue || 'dark');
      }
    });
  }

  setTheme(themeName) {
    if (!this.themes[themeName]) {
      console.error(`Theme "${themeName}" not found`);
      return false;
    }

    this.currentTheme = themeName;
    this.saveTheme(themeName);
    this.applyTheme(themeName);

    // Dispatch custom event
    window.dispatchEvent(
      new CustomEvent('theme-changed', {
        detail: { theme: themeName, colors: this.themes[themeName].colors }
      })
    );

    return true;
  }

  applyTheme(themeName) {
    const theme = this.themes[themeName];
    if (!theme) return;

    // Apply CSS variables
    this.applyCSSVariables(theme.colors);

    // Update HTML class
    document.documentElement.setAttribute('data-theme', themeName);
    document.body.setAttribute('data-theme', themeName);

    // Update theme meta tag
    const metaThemeColor = document.querySelector('meta[name="theme-color"]');
    if (metaThemeColor) {
      metaThemeColor.setAttribute('content', theme.colors.accentPrimary || theme.colors.neonCyan);
    }
  }

  applyCSSVariables(colors) {
    const root = document.documentElement;

    Object.entries(colors).forEach(([key, value]) => {
      // Convert camelCase to kebab-case
      const cssVarName = `--${key.replace(/([A-Z])/g, '-$1').toLowerCase()}`;
      root.style.setProperty(cssVarName, value);
    });
  }

  toggleTheme() {
    const newTheme = this.currentTheme === 'dark' ? 'light' : 'dark';
    this.setTheme(newTheme);
    return newTheme;
  }

  getCurrentTheme() {
    return {
      name: this.currentTheme,
      ...this.themes[this.currentTheme]
    };
  }

  getColor(colorName) {
    return this.themes[this.currentTheme].colors[colorName] || null;
  }

  createCustomTheme(name, colors) {
    this.themes[name] = {
      name: name,
      colors: { ...this.themes.dark.colors, ...colors }
    };
    return this.themes[name];
  }

  exportTheme() {
    return JSON.stringify(this.getCurrentTheme(), null, 2);
  }

  importTheme(themeJSON) {
    try {
      const theme = JSON.parse(themeJSON);
      this.createCustomTheme(theme.name, theme.colors);
      return true;
    } catch (error) {
      console.error('Invalid theme JSON:', error);
      return false;
    }
  }

  // React Hook
  static useTheme() {
    const [theme, setTheme] = React.useState(() => themeManager.getCurrentTheme());

    React.useEffect(() => {
      const handleThemeChange = (e) => {
        setTheme(e.detail);
      };

      window.addEventListener('theme-changed', handleThemeChange);
      return () => window.removeEventListener('theme-changed', handleThemeChange);
    }, []);

    return {
      theme,
      currentTheme: themeManager.currentTheme,
      setTheme: (themeName) => themeManager.setTheme(themeName),
      toggleTheme: () => themeManager.toggleTheme(),
      getColor: (colorName) => themeManager.getColor(colorName)
    };
  }
}

// Create global instance
const themeManager = new ThemeManager();

// Export for use in modules
if (typeof module !== 'undefined' && module.exports) {
  module.exports = themeManager;
}

// Vue 3 Plugin
if (typeof window !== 'undefined') {
  window.$themeManager = {
    install(app, options) {
      app.provide('themeManager', themeManager);
      app.config.globalProperties.$theme = themeManager;
      app.config.globalProperties.$useTheme = () => {
        return {
          currentTheme: themeManager.currentTheme,
          setTheme: (name) => themeManager.setTheme(name),
          toggleTheme: () => themeManager.toggleTheme(),
          getColor: (name) => themeManager.getColor(name),
          getCurrentTheme: () => themeManager.getCurrentTheme()
        };
      };
    }
  };
}
