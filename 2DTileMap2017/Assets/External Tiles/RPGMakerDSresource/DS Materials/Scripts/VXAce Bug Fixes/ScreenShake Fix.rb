#==============================================================================
# ** Game_Interpreter
#------------------------------------------------------------------------------
#  An interpreter for executing event commands. This class is used within the
# Game_Map, Game_Troop, and Game_Event classes.
#------------------------------------------------------------------------------
# Corrective patch courtesy of Hiino
#==============================================================================
 
class Game_Interpreter
  #--------------------------------------------------------------------------
  # * Fixed Screen Shake
  #--------------------------------------------------------------------------
  def command_225
    screen.start_shake(@params[0], @params[1], @params[2])
    wait(@params[2]) if @params[3]
  end
end