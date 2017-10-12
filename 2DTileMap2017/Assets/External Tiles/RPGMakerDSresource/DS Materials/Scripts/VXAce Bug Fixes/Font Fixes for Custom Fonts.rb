#==============================================================================
# ¡ Font Fixes for Custom Fonts
# =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
# Author: Archeia, LoneWolf, Mezzolan
#==============================================================================
# ? ¥ Updates
# =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
# 12/19/2012 - Edited it to be user friendly and compressed.
# 02/14/2012 - Started and Finished Script.
# 
#==============================================================================
# ? ¥ Introduction
# =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
# @This is to fix some custom font problems such as the stray boxes.
# =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

class Game_System
  #--------------------------------------------------------------------------
  # * Determine if Japanese Mode
  #--------------------------------------------------------------------------
  def japanese?
    return false
  end
end

# Add function for temporarily switching a window's current drawing font.
class Font
  def use( window )
        old_font = window.contents.font.dup
        window.contents.font = self
        yield
        window.contents.font = old_font  
  end
end

# Fixes the arrow character (¨) used in places in the UI
# since custom font does not support that character
module Mez
  module ArrowFix
        FONT = Font.new(["VL Gothic"])  # This is the font used for the arrows, checked in order.
  end
end

# For Actor Equip Window
class Window_EquipStatus
  alias mez_wes_dra draw_right_arrow
    def draw_right_arrow(x, y)
        Mez::ArrowFix::FONT.use(self) do
          mez_wes_dra(x, y)
    end
  end
end

# Font Junk fix
class Window_Base
  alias :process_normal_character_vxa :process_normal_character
  def process_normal_character(c, pos)
        return unless c >= ' '
        process_normal_character_vxa(c, pos)
  end
end