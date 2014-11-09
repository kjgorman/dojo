class Colour

  def initialize red, green, blue
    @red = red
    @green = green
    @blue = blue
  end

  def is_red
     @red > @green and @red > @blue
  end

  def is_blue
    @blue > @red and @blue > @green
  end

  def is_green
    @green > @blue and @green > @blue
  end

end
